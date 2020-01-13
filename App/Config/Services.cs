using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using vancil.Framework.Account;

namespace vancil.App.Config
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