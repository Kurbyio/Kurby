using System;
using System.Linq;
using System.Security.Claims;
using Kurby.Models;
using Microsoft.AspNetCore.Http;

namespace Kurby.Internals.Auth
{
    public class AuthManager
    {
        private AppDbContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthManager(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;
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
    }
}