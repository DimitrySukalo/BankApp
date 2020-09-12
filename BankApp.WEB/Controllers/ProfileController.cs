using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BankApp.WEB.Controllers
{
    /// <summary>
    /// Profile controller
    /// </summary>
    [Authorize]
    public class ProfileController : Controller
    {
        /// <summary>
        /// Main profile page
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult Index()
        {
            return View("Profile");
        }
    }
}
