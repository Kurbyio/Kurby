using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using vancil.Framework.Account;
using vancil.Models;
using vancil.ViewModels.Account;

namespace vancil.Controllers.Account
{
    public class AccountController : Controller
    {
        private AppDbContext db;

        public AccountController(AppDbContext db)
        {
            this.db = db;
        }

        public IActionResult Register()
        {
            return View();
        }

        /// <summary>
        /// Registers a new user
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = model.Email;

                PasswordManager pw = new PasswordManager();
                // Unique ID used for hashing
                var hashId = Guid.NewGuid();
                user.HashID = hashId;
                user.Password = pw.Hash(hashId, model.Password);

                db.Users.Add(user);
                db.SaveChanges();

                ViewData["message"] = "User created successfully!";
            }
            return View();
        }

        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        /// <summary>
        /// Processes user login
        /// </summary>
        /// <param name="model"></param>
        /// <param name="returnUrl"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Login(LoginViewModel model,string returnUrl)
        {
            bool isUservalid = false;
            User user = db.Users.Where(usr => usr.Email == model.Email).SingleOrDefault();

            PasswordManager pw = new PasswordManager();
            var isPassword = pw.Verify(user.HashID, user.Password, model.Password);            

            if(isPassword == PasswordVerificationResult.Success)
            {
                isUservalid = true;
            }

            if(ModelState.IsValid && isUservalid)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();
                props.IsPersistent = model.RememberMe;

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["message"] = "Invalid UserName or Password!";
            }

            return View();
        }

        /// <summary>
        /// Handles logout
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            
            return RedirectToAction("Login", "Account");
        }
    }
}