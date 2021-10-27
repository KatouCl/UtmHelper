using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using MediatR;
using Utm.Application;
using Utm.Application.Cqrs.Utms.Commands.UpdateUtm;
using Utm.Application.Repositories.WorkingWithFiles;
using Utm.WebApplication.Models;

namespace Utm.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMediator _mediator;
        private readonly IWorkingWithFilesRepository _workingWithFilesRepository;

        public HomeController(ILogger<HomeController> logger,
            IMediator mediator,
            IWorkingWithFilesRepository workingWithFilesRepository)
        {
            _logger = logger;
            _mediator = mediator;
            _workingWithFilesRepository = workingWithFilesRepository;
        }

        public IActionResult Index()
        {
            var utm = _workingWithFilesRepository.LoadDataWithSettings(ConstApplication.ConfigurationFile);
            return View(utm);
        }

        [HttpPost]
        public IActionResult ApplySettings(UtmDto utm)
        {
            if (!ModelState.IsValid
                && string.IsNullOrWhiteSpace(utm.Address)
                && string.IsNullOrWhiteSpace(utm.Port.ToString()))
            {
                return View("Index", utm);
            }

            _mediator.Send(new UpdateUtmCommand(utm.Address, utm.Port));
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}