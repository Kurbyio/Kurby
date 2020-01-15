using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Kurby.Models;

namespace Kurby.Framework.Account
{
    public class AuthUser
    {
        private AppDbContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthUser(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;
        }

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