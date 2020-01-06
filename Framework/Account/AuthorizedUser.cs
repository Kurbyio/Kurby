using System.Linq;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using vancil.Models;

namespace vancil.Framework.Account
{
    public class AuthorizedUser
    {
        private AppDbContext db;

        public AuthorizedUser(AppDbContext db)
        {
            this.db = db;
        }

        public vancil.Models.User GetUser()
        {           
            vancil.Models.User user = db.Users.FirstOrDefault(u => u.Id == 1);

            return user;
        }
    }
}