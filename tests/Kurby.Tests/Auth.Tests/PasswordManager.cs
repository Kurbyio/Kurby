using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using static Kurby.Tests.Helpers.ConfigurationHelper;

namespace Kurby.Tests.Auth.Tests
{
    public class PasswordManager
    {  
        [Fact]
        public void CheckPasswordHash()
        {
            var config = InitConfiguration();
            var passwordManager = new Internals.Auth.PasswordManager(config);
            var hashedPassword = passwordManager.Hash("testing1!");
            var verifyPassowrd = passwordManager.Verify(hashedPassword, "testing1!");
            
            Assert.True(verifyPassowrd == PasswordVerificationResult.Success);
        }
    }
}
