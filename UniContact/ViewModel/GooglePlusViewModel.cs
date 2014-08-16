using System;
using System.Diagnostics;
using System.Threading;
using Windows.Security.Authentication.Web;
using Cimbalino.Phone.Toolkit.Services;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Auth.OAuth2.Flows;
using Google.Apis.Auth.OAuth2.Responses;
using Google.Apis.Services;
using Microsoft.Phone.Reactive;
using ScottIsAFool.WindowsPhone.ViewModel;

namespace UniContact.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class GooglePlusViewModel : ViewModelBase
    {
        private const string AuthUrl = "https://accounts.google.com/o/oauth2/auth?client_id={0}&redirect_uri={1}&response_type=code&scope={2}";
        private const string EndUrl = "https://accounts.google.com/o/oauth2/approval?";
        private readonly INavigationService _navigationService;

        /// <summary>
        /// Initializes a new instance of the GooglePlusViewModel class.
        /// </summary>
        public GooglePlusViewModel(INavigationService navigationService)
        {
            _navigationService = navigationService;
        }

        public bool IsAuthenticated { get; set; }

        public RelayCommand AuthoriseCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    var url = string.Format(AuthUrl, Uri.EscapeDataString(Constants.GoogleAppId), Uri.EscapeDataString(Constants.GoogleReturnUrl), Uri.EscapeDataString(Google.Apis.Plus.v1.PlusService.Scope.PlusLogin));

                    WebAuthenticationBroker.AuthenticateAndContinue(new Uri(url, UriKind.Absolute), new Uri(Constants.GoogleReturnUrl, UriKind.Absolute));
                });
            }
        }

        public override void WireMessages()
        {
            Messenger.Default.Register<NotificationMessage>(this, m =>
            {
                if (m.Notification.Equals(Constants.Messages.GooglePlusAuthMsg))
                {
                    var clientSecrets = new ClientSecrets
                    {
                        ClientId = Constants.GoogleAppId,
                        ClientSecret = Constants.GoogleAppSecret
                    };

                    var flow = new GoogleAuthorizationCodeFlow(new GoogleAuthorizationCodeFlow.Initializer
                    {
                        ClientSecrets = clientSecrets,
                        Scopes = new[] {Google.Apis.Plus.v1.PlusService.Scope.PlusLogin}
                    });

                    //var userCredentials = new UserCredential(flow, "user", new TokenResponse
                    //{
                        
                    //})
                }
            });
        }
    }
}