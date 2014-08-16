using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows;
using Windows.Foundation.Collections;
using Windows.Security.Authentication.Web;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Newtonsoft.Json;
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
    public class FourSquareViewModel : ViewModelBase
    {
        private const string AuthUrl = "https://foursquare.com/oauth2/authenticate?client_id={0}&response_type=token&redirect_uri={1}";
        private const string FriendsUrl = "https://api.foursquare.com/v2/users/self/friends?limit=500&offset={0}&oauth_token={1}&v=20140523";
        private const string ProfileUrl = "https://api.foursquare.com/v2/users/{0}?oauth_token={1}&v=20140523";
        private readonly INavigationService _navigationService;

        private List<FourSquareFriend> _friends;

        /// <summary>
        /// Initializes a new instance of the FourSquareViewModel class.
        /// </summary>
        public FourSquareViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
            IsAuthenticated = App.AuthenticationService.IsFourSquareAuthenticated;
        }

        public bool IsAuthenticated { get; set; }
        public FourSquareFriend FourSquareFriend { get; set; }

        public RelayCommand AuthoriseCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    var url = string.Format(AuthUrl, Constants.FourSquareAppId, Constants.FourSquareReturnUrl);

                    WebAuthenticationBroker.AuthenticateAndContinue(new Uri(url, UriKind.Absolute), new Uri(Constants.FourSquareReturnUrl), new ValueSet(), WebAuthenticationOptions.None);
                });
            }
        }

        public RelayCommand GetContactsCommand
        {
            get
            {
                return new RelayCommand(async () =>
                {
                    try
                    {
                        SetProgressBar("Getting FourSquare friends...");

                        await GetFriends(0);
                    }
                    catch (Exception ex)
                    {
                        var s = "";
                    }

                    SetProgressBar();
                });
            }
        }

        private async Task GetFriends(int offset)
        {
            var url = string.Format(FriendsUrl, offset, App.AuthenticationService.FourSquareAccessToken);
            Debug.WriteLine(url);
            var response = await App.HttpClient.GetAsync(url);

            Debug.WriteLine(response.StatusCode);
            if (response.StatusCode == HttpStatusCode.OK)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                if (!string.IsNullOrEmpty(responseString))
                {
                    var friendResponse = await JsonConvert.DeserializeObjectAsync<FourSquareResponse<FourSquareUserResponse>>(responseString);
                    if (friendResponse.Response.Friends.Count > 0)
                    {
                        if (_friends == null)
                        {
                            _friends = new List<FourSquareFriend>();
                        }

                        _friends.AddRange(friendResponse.Response.Friends.Items);
                        if (friendResponse.Response.Friends.Count > _friends.Count)
                        {
                            await GetFriends(_friends.Count);
                        }
                        else
                        {
                            var messageBox = new MessageBoxService();
                            var result = await messageBox.ShowAsync(string.Format("We have found {0} user(s), would you like to add them now?", _friends.Count), "Add users", new List<string> { "yes", "no" });

                            if (result == 0)
                            {
                                await ContactService.Current.CheckAndUpdateUsers(_friends.Cast<IContact>().ToList());

                                MessageBox.Show("Done");
                            }
                        }
                    }
                }
            }
        }

        private async Task GetUser(string userId)
        {
            try
            {
                SetProgressBar("Getting user details...");

                var url = string.Format(ProfileUrl, userId, App.AuthenticationService.FourSquareAccessToken);
                Debug.WriteLine(url);
                var response = await App.HttpClient.GetAsync(url);

                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var responseString = await response.Content.ReadAsStringAsync();
                    Debug.WriteLine(responseString);
                    var userResponse = await JsonConvert.DeserializeObjectAsync<FourSquareResponse<FourSquareProfileResponse>>(responseString);
                    FourSquareFriend = userResponse.Response.User;
                }
            }
            catch (Exception ex)
            {
                var s = "";
            }

            SetProgressBar();
        }

        public RelayCommand SignOutCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    App.AuthenticationService.RemoveFourSquareUser();
                    IsAuthenticated = App.AuthenticationService.IsFourSquareAuthenticated;
                });
            }
        }

        public override void WireMessages()
        {
            Messenger.Default.Register<NotificationMessage>(this, async m =>
            {
                if (m.Notification.Equals(Constants.Messages.FourSquareAuthMsg))
                {
                    var query = (string) m.Sender;
                    var authToken = query.Replace("#access_token=", string.Empty);

                    App.AuthenticationService.SaveFourSquareToken(authToken);
                    IsAuthenticated = App.AuthenticationService.IsFourSquareAuthenticated;
                }

                if (m.Notification.Equals(Constants.Messages.FourSquareOpenContactMsg))
                {
                    var userId = (string) m.Sender;

                    await GetUser(userId);
                }
            });
        }
    }
}