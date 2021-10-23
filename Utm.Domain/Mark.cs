using Newtonsoft.Json;

namespace Utm.Domain
{
    public class Mark
    {
        [JsonProperty(PropertyName = "code")]
        public string Code { get; set; }

        [JsonProperty(PropertyName = "owner")] 
        public bool? Owner { get; set; }
    }
}