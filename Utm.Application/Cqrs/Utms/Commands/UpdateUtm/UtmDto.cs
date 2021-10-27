using Newtonsoft.Json;

namespace Utm.Application.Cqrs.Utms.Commands.UpdateUtm
{
    public class UtmDto
    {
        [JsonProperty(PropertyName = "Logging")]
        public object Logging { get; set; }
        
        [JsonProperty(PropertyName = "AllowedHosts")]
        public string AllowedHosts { get; set; }
        
        [JsonProperty(PropertyName = "ServiceAddress")]
        public string Address { get; set; }

        [JsonProperty(PropertyName = "ServicePort")]
        public int Port { get; set; }
    }
}