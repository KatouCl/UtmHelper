using MediatR;
using Newtonsoft.Json;

namespace Utm.Application.Cqrs.Utms.Commands.UpdateUtm
{
    public record UpdateUtmCommand(string Address, int Port) : IRequest
    {
        [JsonProperty(PropertyName = "ServiceAddress")]
        public string Address { get; set; } = Address;

        [JsonProperty(PropertyName = "ServicePort")]
        public int Port { get; set; } = Port;
    }
}