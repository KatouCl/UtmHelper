using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Utm.Application.Cqrs.Utms.Commands.UpdateUtm;

namespace Utm.Application.Repositories.WorkingWithFiles
{
    public class WorkingWithFilesService : IWorkingWithFilesRepository
    {
        public IEnumerable<string> ReadFileAndAddToList(IFormFile file)
        {
            var resultList = new List<string>();
            // var result = new StringBuilder();
            using var reader = new StreamReader(file.OpenReadStream());

            while (reader.Peek() >= 0)
                //result.AppendLine(await reader.ReadLineAsync());
                resultList.Add(reader.ReadLine());

            return resultList;
        }

        public string FilePath(string nameFile)
        {
            if (string.IsNullOrWhiteSpace(nameFile))
                return "";

            var pathContentRoot = Directory.GetCurrentDirectory();
            var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
            pathContentRoot = Path.GetDirectoryName(pathToExe);

            var filePath = Path.Combine(pathContentRoot, nameFile);

            return filePath;
        }

        public string FileToString(string filePath)
        {
            if (!File.Exists(filePath))
                return String.Empty;

            using var streamReader = new StreamReader(filePath);
            var stringResult = streamReader.ReadToEnd();

            return stringResult;
        }

        public bool SaveFileAsJson(string filePath, object model)
        {
            if (!File.Exists(filePath))
                return false;

            var output = JsonConvert.SerializeObject(model, Formatting.Indented);
            using (var sw = new StreamWriter(filePath, false, System.Text.Encoding.Default))
            {
                sw.WriteLine(output);
            }

            return true;
        }

        public UtmDto LoadDataWithSettings(string fileName)
        {
            var filePath = FilePath(fileName);
            var resultJson = FileToString(filePath);
            if (resultJson == null)
                return new UtmDto
                {
                    Address = "Localhost",
                    Port = 8080
                };

            var deserializeUtm = JsonConvert.DeserializeObject<UtmDto>(resultJson);
            return deserializeUtm;
        }
    }
}