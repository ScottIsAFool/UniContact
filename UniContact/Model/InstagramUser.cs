using Newtonsoft.Json;
using PropertyChanged;

namespace UniContact.Model
{
    [ImplementPropertyChanged]
    public class InstagramUser : IContact
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("username")]
        public string Username { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("profile_picture")]
        public string ProfilePicture { get; set; }

        [JsonIgnore]
        public string ContactId
        {
            get { return string.Format("Instagram.{0}", Id); }
        }

        public string WebAddress
        {
            get { return string.Format("http://instagram.com/{0}", Username); }
            set { }
        }

        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Bio { get; set; }
    }
}