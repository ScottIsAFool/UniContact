using System.Collections.Generic;
using Newtonsoft.Json;
using PropertyChanged;

namespace UniContact.Model
{
    [ImplementPropertyChanged]
    public class Pagination
    {
        [JsonProperty("next_url")]
        public string NextUrl { get; set; }

        [JsonProperty("next_cursor")]
        public string NextCursor { get; set; }
    }

    [ImplementPropertyChanged]
    public class Meta
    {
        [JsonProperty("code")]
        public int Code { get; set; }
    }

    [ImplementPropertyChanged]
    public class InstagramFollowsResponse
    {
        [JsonProperty("pagination")]
        public Pagination Pagination { get; set; }

        [JsonProperty("meta")]
        public Meta Meta { get; set; }

        [JsonProperty("data")]
        public List<InstagramUser> Users { get; set; }
    }

    public class InstagramUserResponse
    {
        [JsonProperty("data")]
        public InstagramUser User { get; set; }
    }
}
