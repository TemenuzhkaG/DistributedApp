

using Newtonsoft.Json;

namespace WebAPI.ResponseHelper
{
    public class Message
    {
        [JsonProperty(PropertyName = "code")]
        public object Code { get; set; }

        [JsonProperty(PropertyName = "body", NullValueHandling = NullValueHandling.Ignore)]
        public object Body { get; set; }

        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public object Error { get; set; }
    }
}
