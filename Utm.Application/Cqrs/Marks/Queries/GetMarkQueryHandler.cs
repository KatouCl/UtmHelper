using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Utm.Application.Repositories.Rest;
using Utm.Application.Repositories.WorkingWithFiles;

namespace Utm.Application.Cqrs.Marks.Queries
{
    public class GetMarkQueryHandler
    {
        public class Hadler : IRequestHandler<MarkQuery, IEnumerable<MarkLookupDto>>
        {
            private readonly IRestRepository _restRepository;
            private readonly IWorkingWithFilesRepository _workingWithFilesRepository;

            public Hadler(IRestRepository restRepository,
                IWorkingWithFilesRepository workingWithFilesRepository) =>
                (_restRepository, _workingWithFilesRepository) = (restRepository, workingWithFilesRepository);


            public async Task<IEnumerable<MarkLookupDto>> Handle(MarkQuery request, CancellationToken cancellationToken)
            {
                var utm = _workingWithFilesRepository.LoadDataWithSettings(ConstApplication.ConfigurationFile);
                var markLookupList = new List<MarkLookupDto>();
                foreach (var item in request.Codes)
                {
                    var resultJsonRequest =
                        _restRepository.GetJsonRequest($"http://{utm.Address}:{utm.Port}/",
                            $"{ConstApplication.ResourceMarkUrl}{item}");

                    var deserializeJson = JsonConvert.DeserializeObject<MarkLookupDto>(resultJsonRequest);

                    markLookupList.Add(deserializeJson);
                }

                return markLookupList;
            }
        }
    }
}