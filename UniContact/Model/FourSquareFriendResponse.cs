using Newtonsoft.Json;
using PropertyChanged;

namespace UniContact.Model
{
    [ImplementPropertyChanged]
    public class Photo
    {
        [JsonProperty("prefix")]
        public string Prefix { get; set; }

        [JsonProperty("suffix")]
        public string Suffix { get; set; }

        [JsonIgnore]
        public string ImageUrl
        {
            get { return string.Format("{0}256x256{1}", Prefix, Suffix); }
        }
    }

    [ImplementPropertyChanged]
    public class Contact
    {
        [JsonProperty("email")]
        public string Email { get; set; }

        [JsonProperty("twitter")]
        public string Twitter { get; set; }

        [JsonProperty("facebook")]
        public string Facebook { get; set; }

        [JsonProperty("phone")]
        public string Phone { get; set; }
    }

    [ImplementPropertyChanged]
    public class FourSquareFriend : IContact
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        public string Username { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
            set { }
        }

        public string ProfilePicture
        {
            get { return Photo != null ? Photo.ImageUrl : string.Empty; }
            set { }
        }

        public string ContactId
        {
            get { return string.Format("foursquare.{0}", Id); }
        }

        public string WebAddress
        {
            get { return string.Format("https://foursqaure.com/u/{0}", Id); }
            set{}
        }

        public string EmailAddress
        {
            get { return Contact != null ? Contact.Email : string.Empty; }
            set { }
        }

        [JsonProperty("firstName")]
        public string FirstName { get; set; }

        [JsonProperty("lastName")]
        public string LastName { get; set; }

        [JsonProperty("gender")]
        public string Gender { get; set; }

        [JsonProperty("relationship")]
        public string Relationship { get; set; }

        [JsonProperty("photo")]
        public Photo Photo { get; set; }

        [JsonProperty("homeCity")]
        public string HomeCity { get; set; }

        [JsonProperty("bio")]
        public string Bio { get; set; }

        [JsonProperty("contact")]
        public Contact Contact { get; set; }

        public string PhoneNumber
        {
            get { return Contact != null ? Contact.Phone : string.Empty; }
            set { }
        }
    }

    [ImplementPropertyChanged]
    public class Friends
    {
        [JsonProperty("count")]
        public int Count { get; set; }

        [JsonProperty("items")]
        public FourSquareFriend[] Items { get; set; }
    }

    [ImplementPropertyChanged]
    public class FourSquareUserResponse
    {

        [JsonProperty("friends")]
        public Friends Friends { get; set; }

        [JsonProperty("checksum")]
        public string Checksum { get; set; }
    }

    [ImplementPropertyChanged]
    public class FourSquareProfileResponse
    {
        [JsonProperty("user")]
        public FourSquareFriend User { get; set; }
    }
}
