using Microsoft.AspNetCore.Mvc;

namespace LibrarySystem.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AddBook()
        {
            return View();
        }

        public IActionResult BorrowBook()
        {
            return View();
        }

        public IActionResult ViewReport()
        {
            return View();
        }

        public IActionResult ManageSections()
        {
            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        public IActionResult FAQ()
        {
            return View();
        }
    }
}