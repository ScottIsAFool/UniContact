using System;
using System.Windows;
using Windows.Phone.PersonalInformation;
using Microsoft.Phone.Controls;

namespace UniContact.Views
{
    public partial class MainPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();

            // Sample code to localize the ApplicationBar
            //BuildLocalizedApplicationBar();
        }

        private async void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            //DoItButton.IsEnabled = false;

            //var manager = await ContactBindings.GetAppContactBindingManagerAsync();

            //var contact = manager.CreateContactBinding(DateTime.Now.ToShortTimeString());
            //contact.FirstName = "Mel";
            //contact.LastName = "Sheppard";
            //contact.EmailAddress1 = "msheppard27@gmail.com";
            //contact.Name = "Mel Sheppard";

            //try
            //{
            //    await manager.SaveContactBindingAsync(contact);
            //}
            //catch (Exception ex)
            //{

            //}

            //var store = await ContactStore.CreateOrOpenAsync();
            //var contact = new StoredContact(store);

            //contact.RemoteId = DateTime.Now.ToShortTimeString();
            //contact.GivenName = "Mel";
            //contact.FamilyName = "Sheppard";
            //contact.DisplayName = "Mel Sheppard";

            //var properties = await contact.GetPropertiesAsync();
            //properties.Add(KnownContactProperties.Email, "msheppard27@gmail.com");

            //var extendedProperties = await contact.GetExtendedPropertiesAsync();
            //extendedProperties.Add("Google+", "msheppard");

            //await contact.SaveAsync();

            //var query = store.CreateContactQuery();
            //var contacts = await query.GetContactsAsync();

            //var list = contacts.ToList();

            //DoItButton.IsEnabled = true;
        }

        private void SettingsButton_OnClick(object sender, EventArgs e)
        {
            NavigationService.Navigate(new Uri("/Views/SettingsView.xaml", UriKind.Relative));
        }
    }
}