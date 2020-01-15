using Kurby.Framework.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace Kurby.App.Config
{
    public static class Services
    {
        /// <summary>
        /// Adds dependency injection items to the container
        /// </summary>
        /// <param name="services"></param>
        public static void SetServices(this IServiceCollection services)
        {
            // Scopes
            services.AddScoped<AuthUser>();

            // Singletons
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Transients
            
        }
    }
}