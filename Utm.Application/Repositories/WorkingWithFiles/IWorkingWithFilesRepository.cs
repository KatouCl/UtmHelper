using System.Collections.Generic;
using Microsoft.AspNetCore.Http;
using Utm.Application.Cqrs.Utms.Commands.UpdateUtm;

namespace Utm.Application.Repositories.WorkingWithFiles
{
    public interface IWorkingWithFilesRepository
    {
        IEnumerable<string> ReadFileAndAddToList(IFormFile file);
        string FilePath(string nameFile);
        string FileToString(string filePath);
        bool SaveFileAsJson(string filePath, object model);
        UtmDto LoadDataWithSettings(string fileName);
    }
}