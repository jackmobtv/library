using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;
using WebPresentation.Models;

namespace WebPresentation.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly UserManager<IdentityUser> _identityManager;

        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> identityManager)
        {
            _logger = logger;
            _identityManager = identityManager;
        }

        public IActionResult Index()
        {
            return View();
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

        public ActionResult AccessDenied()
        {
            return View();
        }

        public string GetUserEmail()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                return null;
            }

            var user = _identityManager.FindByIdAsync(userId).Result;

            if (user == null)
            {
                return null;
            }

            return user.Email;
        }
    }
}
