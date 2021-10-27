using System.Collections.Generic;
using System.Linq;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Utm.Application.Cqrs.Marks.Queries;
using Utm.Application.Repositories.WorkingWithFiles;

namespace Utm.WebApplication.Controllers
{
    public class CheckingStampsController : Controller
    {
        private readonly IWorkingWithFilesRepository _workingWithFilesRepository;
        private readonly IMediator _mediator;

        private IEnumerable<MarkLookupDto> _markLists { get; set; }

        public CheckingStampsController(IWorkingWithFilesRepository workingWithFilesRepository, IMediator mediator)
        {
            _workingWithFilesRepository = workingWithFilesRepository;
            _mediator = mediator;
        }


        // GET
        public IActionResult Index()
        {
            // ViewData["CountMarks"] = _markLists.Count() == null ;
            return View();
        }

        [HttpPost]
        [RequestFormLimits(MultipartBodyLengthLimit = 104857600)]
        public IActionResult UploadFile(IFormFile uploadFile)
        {
            if (!ModelState.IsValid || uploadFile == null || uploadFile.Length == 0)
                return View("Index");

            var listCodes = _workingWithFilesRepository.ReadFileAndAddToList(uploadFile);
            var responce = _mediator.Send(new MarkQuery(listCodes));
            _markLists = responce.Result;

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult SearchMark(string searchQuery)
        {
            return View("Index");
        }
    }
}