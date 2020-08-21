using Microsoft.AspNetCore.Mvc;

namespace BankApp.WEB.Controllers
{
    public class RegisterController : Controller
    {
        /// <summary>
        /// Register form
        /// </summary>
        [HttpGet]
        public IActionResult Index()
        {
            return View("Register");
        }
    }
}
