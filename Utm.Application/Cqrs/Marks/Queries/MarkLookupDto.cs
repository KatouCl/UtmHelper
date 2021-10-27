using Newtonsoft.Json;

namespace Utm.Application.Cqrs.Marks.Queries
{
    public class MarkLookupDto
    {
        [JsonProperty(PropertyName = "code")] 
        public string Code { get; set; }

        [JsonProperty(PropertyName = "owner")] 
        public bool? Owner { get; set; }
    }
}