using Newtonsoft.Json;
using PropertyChanged;

namespace UniContact.Model
{
    [ImplementPropertyChanged]
    public class InstagramTokenResponse
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("user")]
        public InstagramUser User { get; set; }
    }
}
