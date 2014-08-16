using System;
using System.Windows.Navigation;
using Cimbalino.Phone.Toolkit.Extensions;
using GalaSoft.MvvmLight.Ioc;
using GalaSoft.MvvmLight.Messaging;
using UniContact.ViewModel;

namespace UniContact
{
    public class CustomUriMapper : UriMapperBase
    {
        // /PeopleExtension?action=Show_Contact&contact_ids=Instagram.18466118
        public override Uri MapUri(Uri uri)
        {
            var tempUri = uri.ToString();
            if (tempUri.StartsWith("/PeopleExtension"))
            {
                var peopleUrl = new Uri("http://ferretlabs.com" + tempUri, UriKind.Absolute);
                var queryStrings = peopleUrl.QueryString();
                var contactId = queryStrings["contact_ids"];
                if (contactId.StartsWith("Instagram"))
                {
                    if (!App.AuthenticationService.IsInstagramAuthenticated)
                    {
                        return new Uri("/Views/MainPage.xaml", UriKind.Relative);
                    }

                    if (SimpleIoc.Default.GetInstance<InstagramViewModel>() != null)
                    {
                        Messenger.Default.Send(new NotificationMessage(contactId.Replace("Instagram.", string.Empty), Constants.Messages.InstagramOpenContactMsg));

                        return new Uri("/Views/Contact/InstagramContactView.xaml", UriKind.Relative);
                    }
                }
                if (contactId.StartsWith("foursquare"))
                {
                    if (!App.AuthenticationService.IsFourSquareAuthenticated)
                    {
                        return new Uri("/Views/MainPage.xaml", UriKind.Relative);
                    }

                    if (SimpleIoc.Default.GetInstance<FourSquareViewModel>() != null)
                    {
                        Messenger.Default.Send(new NotificationMessage(contactId.Replace("foursquare.", string.Empty), Constants.Messages.FourSquareOpenContactMsg));

                        return new Uri("/Views/Contact/FourSquareContactView.xaml", UriKind.Relative);
                    }
                }
            }

            return uri;
        }
    }
}
