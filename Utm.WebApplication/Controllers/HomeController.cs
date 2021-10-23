using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Utm.Domain;
using Utm.WebApplication.Models;

namespace Utm.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private List<Mark> _listObjs = new List<Mark>();
        private List<string> _listCodes = new List<string>();

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var strCode =
                "136300040395551018001JRQLALMI4GRZANQODTAL3TAZE47ZEZOKIMM5V6PNG2HFK6ZUJVALWPEQQDZNDU5RZVSGLY6NTWF4RYLMY4U3UKDGXJAYPTJ3Z3FOOOHCZWXHRDBV2BUAJPWZDTQ5RCEPI";
            try
            {
                await SendRequest(client,
                    $"http://" + "26.233.17.76" + ":" + "8080" +
                    "/api/mark/check?code=" + strCode);
            }
            catch (Exception exception)
            {
                exception.Message.ToString();
            }

            return View();
        }


        private async Task SendRequest(HttpClient client, string url)
        {
            var response = await client.GetAsync(url);
            var str = await response.Content.ReadAsStringAsync();

            var jsonData =
                "{\"code\":\"136300040395551018001JRQLALMI4GRZANQODTAL3TAZE47ZEZOKIMM5V6PNG2HFK6ZUJVALWPEQQDZNDU5RZVSGLY6NTWF4RYLMY4U3UKDGXJAYPTJ3Z3FOOOHCZWXHRDBV2BUAJPWZDTQ5RCEPI\"," +
                "\"Owner\":true}";

            var jsonMark = JsonConvert.DeserializeObject<Mark>(str);
            _listObjs.Add(jsonMark);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}