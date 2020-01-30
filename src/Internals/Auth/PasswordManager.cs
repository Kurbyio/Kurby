using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Kurby.Internals.Auth
{
    public class PasswordManager
    {
        public PasswordManager(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        /// <summary>
        /// Hashes password provided
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public string Hash(string password)
        {
            PasswordHasher<string> pw = new PasswordHasher<string>();

            return pw.HashPassword(Configuration.GetSection("ApplicationKey").ToString(), password);
        }

        /// <summary>
        /// Verifies the password supplied is correct
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <param name="suppliedPassword"></param>
        /// <returns></returns>
        public PasswordVerificationResult Verify(string password, string suppliedPassword)
        {
            PasswordHasher<string> pw = new PasswordHasher<string>();

            return pw.VerifyHashedPassword(Configuration.GetSection("ApplicationKey").ToString(), password, suppliedPassword);            
        }
    }
}