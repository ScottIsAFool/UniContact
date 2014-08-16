using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
using ScottIsAFool.WindowsPhone.Extensions;
using ScottIsAFool.WindowsPhone.ViewModel;
using UniContact.Model;
using UniContact.Services;

namespace UniContact.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class InstagramViewModel : ViewModelBase
    {
        private const string AuthUrl = "https://api.instagram.com/oauth/authorize/?client_id={0}&redirect_uri={1}&response_type=code";//&scope=relationships";
        private const string TokenUrl = "https://api.instagram.com/oauth/access_token";
        private const string FollowsUrl = "https://api.instagram.com/v1/users/{0}/follows?access_token={1}";
        private const string UserUrl = "https://api.instagram.com/v1/users/{0}/?access_token={1}";
        private readonly INavigationService _navigationService;

        private string _code;
        private List<InstagramUser> _users;

        /// <summary>
        /// Initializes a new instance of the InstagramViewModel class.
        /// </summary>
        public InstagramViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;

            IsAuthenticated = App.AuthenticationService.IsInstagramAuthenticated;
        }

        public bool IsAuthenticated { get; set; }
        public InstagramUser InstagramUser { get; set; }

        public RelayCommand AuthoriseCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var url = string.Format(AuthUrl, Constants.InstagramAppId, Constants.InstagramReturnUrl);

                    var values = new ValueSet();

                    WebAuthenticationBroker.AuthenticateAndContinue(new Uri(url, UriKind.Absolute), new Uri(Constants.InstagramReturnUrl), values, WebAuthenticationOptions.None);
                });
            }
        }

        public RelayCommand GetContactsCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var url = string.Format(FollowsUrl, App.AuthenticationService.InstagramTokenResponse.User.Id, App.AuthenticationService.InstagramTokenResponse.AccessToken);
                    await GetFriends(url);
                });
            }
        }

        private async Task GetFriends(string url)
        {
            try
            {
                Debug.WriteLine(url);
                SetProgressBar("Getting the people you follow on Instagram...");

                var response = await App.HttpClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();

                    var friendsResponse = await JsonConvert.DeserializeObjectAsync<InstagramFollowsResponse>(responseString);

                    if (!friendsResponse.Users.IsNullOrEmpty())
                    {
                        if (_users == null)
                        {
                            _users = new List<InstagramUser>();
                        }

                        _users.AddRange(friendsResponse.Users);

                        if (friendsResponse.Pagination != null && !string.IsNullOrEmpty(friendsResponse.Pagination.NextUrl))
                        {
                            await GetFriends(friendsResponse.Pagination.NextUrl);
                        }
                        else
                        {
                            var messageBox = new MessageBoxService();
                            var result = await messageBox.ShowAsync(string.Format("We have found {0} user(s), would you like to add them now?", _users.Count), "Add users", new List<string> { "yes", "no" });

                            if (result == 0)
                            {
                                await ContactService.Current.CheckAndUpdateUsers(_users.Cast<IContact>().ToList());

                                MessageBox.Show("Done");
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("GetContactsCommand", ex);
            }

            SetProgressBar();
        }

        private async Task GetUser(string userId)
        {
            var url = string.Format(UserUrl, userId, App.AuthenticationService.InstagramTokenResponse.AccessToken);
            Debug.WriteLine(url);

            SetProgressBar("Getting user details...");
            try
            {
                var response = await App.HttpClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    var userResponse = await JsonConvert.DeserializeObjectAsync<InstagramUserResponse>(responseString);

                    if (userResponse != null)
                    {
                        InstagramUser = userResponse.User;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("GetUser()", ex);
            }

            SetProgressBar();
        }

        public RelayCommand SignOutCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    IsAuthenticated = false;
                    _code = string.Empty;
                    App.AuthenticationService.RemoveInstagramUser();
                });
            }
        }

        private async Task GetAuthenticationToken(string code)
        {
            SetProgressBar("Talking to Instagram...");
            var para = GetAuthParams(code);

            try
            {
                var response = await App.HttpClient.PostAsync(TokenUrl, new FormUrlEncodedContent(para));

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    if (!string.IsNullOrEmpty(responseString))
                    {
                        var userResponse = await JsonConvert.DeserializeObjectAsync<InstagramTokenResponse>(responseString);
                        App.AuthenticationService.SaveInstagramUser(userResponse);

                        IsAuthenticated = true;
                    }
                }
            }
            catch (Exception ex)
            {
                Log.ErrorException("GetAuthenticationToken()", ex);
            }

            SetProgressBar();
        }

        private static Dictionary<string, string> GetAuthParams(string code)
        {
            return new Dictionary<string, string>
            {
                {"client_id", Constants.InstagramAppId},
                {"client_secret", Constants.InstagramAppSecret},
                {"grant_type", "authorization_code"},
                {"redirect_uri", Constants.InstagramReturnUrl},
                {"code", code}
            };
        }

        public override void WireMessages()
        {
            Messenger.Default.Register<NotificationMessage>(this, async m =>
            {
                if (m.Notification.Equals(Constants.Messages.InstagramAuthMsg))
                {
                    if (!string.IsNullOrEmpty(_code))
                    {
                        return;
                    }

                    var queryString = (string) m.Sender;
                    _code = queryString.Replace("?code=", string.Empty);

                    await GetAuthenticationToken(_code);
                }

                if (m.Notification.Equals(Constants.Messages.InstagramOpenContactMsg))
                {
                    var userId = (string) m.Sender;

                    await GetUser(userId);
                }
            });
        }
    }
}