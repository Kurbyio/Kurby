using System;
using System.Linq;
using Kurby.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Kurby.Internals.Auth;
using Kurby.ViewModels.Account;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace Kurby.Controllers.Account
{
    public class AccountController : Controller
    {
        private AppDbContext db;
        private PasswordManager _passwordManager;

        public AccountController(AppDbContext db, PasswordManager passwordManager)
        {
            this.db = db;
            _passwordManager = passwordManager;
        }

        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                user.Email = model.Email;
                user.Password = _passwordManager.Hash(model.Password);

                db.Users.Add(user);
                db.SaveChanges();

                ViewData["message"] = "User created successfully!";
            }
            return View();
        }

        [AllowAnonymous]
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
        [AllowAnonymous]
        public IActionResult Login(LoginViewModel model,string returnUrl)
        {
            bool isUservalid = false;
            User user = db.Users.Where(usr => usr.Email == model.Email).FirstOrDefault();            
            
            var isPassword = _passwordManager.Verify(user.Password, model.Password);            

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

                return RedirectToAction("Index", "Dashboard");
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