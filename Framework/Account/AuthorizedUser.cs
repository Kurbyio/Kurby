using System;
using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using kurby.Models;

namespace kurby.Framework.Account
{
    public class AuthorizedUser
    {
        private AppDbContext db;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizedUser(AppDbContext db, IHttpContextAccessor httpContextAccessor)
        {
            this.db = db;
            _httpContextAccessor = httpContextAccessor;
        }

        public kurby.Models.User GetUser()
        {        
            kurby.Models.User user = null;   
            if(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier) != null){
                user = db.Users.FirstOrDefault(u => u.Id == Int32.Parse(_httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier)));
            }          

            return user;
        }
    }
}