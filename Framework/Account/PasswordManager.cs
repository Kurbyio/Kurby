using System;
using Microsoft.AspNetCore.Identity;

namespace vancil.Framework.Account
{
    public class PasswordManager
    {
        /// <summary>
        /// Hashes password provided
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Hash(Guid user, string password)
        {
            PasswordHasher<string> pw = new PasswordHasher<string>();

            return pw.HashPassword(user.ToString(), password);
        }

        /// <summary>
        /// Verifies the password supplied is correct
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="suppliedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult Verify(Guid user, string password, string suppliedPassword)
        {
            PasswordHasher<string> pw = new PasswordHasher<string>();

            return pw.VerifyHashedPassword(user.ToString(), password, suppliedPassword);            
        }
    }
}