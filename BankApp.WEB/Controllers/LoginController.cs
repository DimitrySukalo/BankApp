using Microsoft.AspNetCore.Mvc;

namespace BankApp.WEB.Controllers
{
    public class LoginController : Controller
    {
        /// <summary>
        /// Login form
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View("Login");
        }
    }
}
