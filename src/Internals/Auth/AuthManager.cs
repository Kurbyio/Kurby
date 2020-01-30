using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Kurby.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace Kurby.Internals.Auth
{
    public class AuthManager
    {
        private AppDbContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        private PasswordManager _passwordManager;

        public AuthManager(AppDbContext db, IHttpContextAccessor httpContextAccessor, PasswordManager passwordManager)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;
            _passwordManager = passwordManager;
        }

        /// <summary>
        /// Gets the information for the currently logged in user
        /// </summary>
        /// <returns></returns>
        public User GetUser()
        {
            User user = null;   
            if(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) != null){
                user = db.Users.FirstOrDefault(u => u.Id == Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }       

            return user;
        }

        public bool Attempt(string email, string password)
        {
            bool isUservalid = false;

            User user = db.Users.Where(usr => usr.Email == email).FirstOrDefault(); 

            var isPassword = _passwordManager.Verify(user.Password, password); 

            if(isPassword == PasswordVerificationResult.Success)
            {
                isUservalid = true;
            }   

            if(isUservalid)
            {
                var claims = new List<Claim>();

                claims.Add(new Claim(ClaimTypes.Email, user.Email));
                claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()));

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                var principal = new ClaimsPrincipal(identity);

                var props = new AuthenticationProperties();

                _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();

                return true;
            }

            return false;
        }

        public bool Check()
        {
            bool isAuthenticated = false;

            if(_httpContextAccessor.HttpContext.User.Identity.IsAuthenticated){
                isAuthenticated = true;
            }

            return isAuthenticated;
        }
    }
}