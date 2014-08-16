using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Windows.Phone.PersonalInformation;
using Cimbalino.Phone.Toolkit.Extensions;
using Cimbalino.Phone.Toolkit.Services;
using ScottIsAFool.WindowsPhone.Extensions;
using UniContact.Extensions;
using UniContact.Model;
using System.IO;

namespace UniContact.Services
{
    public class ContactService
    {
        private static ContactService _current;
        private ContactStore _store;
        private readonly IAsyncStorageService _storagaeService = new AsyncStorageService();

        public static ContactService Current
        {
            get { return _current ?? (_current = new ContactService()); }
        }

        public async Task StartService()
        {
            _store = await ContactStore.CreateOrOpenAsync();
        }

        public async Task CheckAndUpdateUsers(List<IContact> contacts)
        {
            if (contacts.IsNullOrEmpty())
            {
                return;
            }

            foreach (var contact in contacts)
            {
                var existingContact = await _store.FindContactByRemoteIdAsync(contact.ContactId)
                    ?? new StoredContact(_store) {RemoteId = contact.ContactId};

                existingContact.DisplayName = string.IsNullOrEmpty(contact.FullName) ? contact.Username : contact.FullName;
                if (!string.IsNullOrEmpty(contact.FirstName))
                {
                    existingContact.GivenName = contact.FirstName;
                }

                if (!string.IsNullOrEmpty(contact.LastName))
                {
                    existingContact.FamilyName = contact.LastName;
                }

                var contactProperties = await existingContact.GetPropertiesAsync();

                contactProperties.AddIfNotNull(KnownContactProperties.Email, contact.EmailAddress);
                contactProperties.AddIfNotNull(KnownContactProperties.Url, contact.WebAddress);
                contactProperties.AddIfNotNull(KnownContactProperties.Telephone, contact.PhoneNumber);
                contactProperties.AddIfNotNull(KnownContactProperties.Notes, contact.Bio);

                if (!string.IsNullOrEmpty(contact.ProfilePicture) && existingContact.DisplayPicture == null && App.Settings.DownloadDisplayPictures)
                {
                    using (var stream = await App.HttpClient.GetStreamAsync(contact.ProfilePicture))
                    {
                        await existingContact.SetDisplayPictureAsync(stream.AsInputStream());
                    }
                }

                await existingContact.SaveAsync();
            }
        }
    }
}
