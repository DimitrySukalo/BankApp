using Microsoft.AspNetCore.Mvc;

namespace BankApp.WEB.Controllers
{
    public class HomeController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }
    }
}
