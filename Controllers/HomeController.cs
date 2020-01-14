using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using kurby.Framework.Account;
using kurby.Models;

namespace kurby.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        public IActionResult Index()
        {            
            return View();
        }
    }
}
