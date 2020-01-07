using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using vancil.Framework.Account;
using vancil.Models;

namespace vancil.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly AuthorizedUser _authorizedUser;
        private AppDbContext db;
        public HomeController(ILogger<HomeController> logger, AppDbContext db, AuthorizedUser authorizedUser)
        {
            _logger = logger;
            this.db = db;
            _authorizedUser = authorizedUser;

        }

        [Authorize]
        public IActionResult Index()
        {
            var user = _authorizedUser.GetUser();

            string userName = HttpContext.User.FindFirstValue(ClaimTypes.Email);

            ViewData["username"] = user.Name;
            
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
    }
}
