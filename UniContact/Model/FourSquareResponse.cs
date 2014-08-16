using Newtonsoft.Json;
using PropertyChanged;

namespace UniContact.Model
{
    [ImplementPropertyChanged]
    public class FourSquareResponse<TResponseType> where TResponseType : class
    {
        [JsonProperty("response")]
        public TResponseType Response { get; set; }
    }
}
