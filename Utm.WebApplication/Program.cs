using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting.WindowsServices;
using Newtonsoft.Json.Linq;

namespace Utm.WebApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var isService = false;

            if (Debugger.IsAttached == false && args.Contains("--service"))
            {
                isService = true;
            }

            if (isService)
            {
                var pathContentRoot = Directory.GetCurrentDirectory();

                const string configurationFile = "appsettings.json";
                var portNo = "5001";

                var pathToExe = Process.GetCurrentProcess().MainModule.FileName;
                pathContentRoot = Path.GetDirectoryName(pathToExe);

                var appJsonFilePath = Path.Combine(pathContentRoot, configurationFile);

                if (File.Exists(appJsonFilePath))
                {
                    using (StreamReader streamReader = new StreamReader(appJsonFilePath))
                    {
                        var jsonData = streamReader.ReadToEnd();
                        var jObject = JObject.Parse(jsonData);
                        if (jObject["ServicePort"] != null)
                        {
                            portNo = jObject["ServicePort"].ToString();
                        }
                    }
                }

                var host = WebHost.CreateDefaultBuilder(args)
                    .UseContentRoot(pathContentRoot)
                    .UseStartup<Startup>()
                    .UseUrls("http://localhost:" + portNo)
                    .Build();

                host.RunAsService();
            }
            else
            {
                CreateHostBuilder(args).Build().Run();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
    }
}