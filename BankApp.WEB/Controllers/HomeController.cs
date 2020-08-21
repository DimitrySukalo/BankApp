using Microsoft.AspNetCore.Mvc;

namespace BankApp.WEB.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Main home page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View("Index");
        }
    }
}
