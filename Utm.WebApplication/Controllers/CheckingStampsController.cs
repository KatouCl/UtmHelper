using Microsoft.AspNetCore.Mvc;

namespace Utm.WebApplication.Controllers
{
    public class CheckingStampsController : Controller
    {
        // GET
        public IActionResult Index()
        {
            return View();
        }
    }
}