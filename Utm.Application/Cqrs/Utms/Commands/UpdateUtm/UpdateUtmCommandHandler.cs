using System.IO;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using Newtonsoft.Json;
using Utm.Application.Repositories.WorkingWithFiles;

namespace Utm.Application.Cqrs.Utms.Commands.UpdateUtm
{
    public class UpdateUtmCommandHandler
        : IRequestHandler<UpdateUtmCommand>
    {
        private readonly IWorkingWithFilesRepository _workingWithFilesRepository;

        public UpdateUtmCommandHandler(IWorkingWithFilesRepository workingWithFilesRepository)
            => _workingWithFilesRepository = workingWithFilesRepository;

        public async Task<Unit> Handle(UpdateUtmCommand request,
            CancellationToken cancellationToken)
        {
            var filePath = _workingWithFilesRepository.FilePath(ConstApplication.ConfigurationFile);
            var jsonData = _workingWithFilesRepository.FileToString(filePath);

            var deserializeUtm = JsonConvert.DeserializeObject<UtmDto>(jsonData);
            deserializeUtm.Address = request.Address;
            deserializeUtm.Port = request.Port;

            _workingWithFilesRepository.SaveFileAsJson(filePath, deserializeUtm);

            return Unit.Value;
        }
    }
}