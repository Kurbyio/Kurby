using kurby.Framework.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace kurby.App.Config
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
            services.AddScoped<AuthorizedUser>();

            // Singletons
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Transients
            
        }
    }
}