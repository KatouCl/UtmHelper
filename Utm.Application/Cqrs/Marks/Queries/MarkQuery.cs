using System.Collections.Generic;
using MediatR;

namespace Utm.Application.Cqrs.Marks.Queries
{
    public record MarkQuery(IEnumerable<string> Codes) : IRequest<IEnumerable<MarkLookupDto>>
    {
        public IEnumerable<string> Codes { get; } = Codes;
    }
}