using Kurby.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Kurby.Internals.Helpers.DatabaseHelper;
using Microsoft.Extensions.DependencyInjection;

namespace Kurby.App.Config
{
    public class Database
    {
        /// <summary>
        /// Creates the database connection based on user input in the appsettings
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        public void SetDatabase(IServiceCollection services, IConfiguration configuration)
        {
            var databaseType = configuration.GetSection("Database:DatabaseType");

            if (databaseType.Value.ToLower() == "mysql")
            {
                var connectionStringHelper = new ConnectionStringHelper();
                var connectionString = connectionStringHelper.CreateConnectionString(configuration);
                services.AddDbContext<AppDbContext>(options => options
                    .UseMySql(connectionString)
                );
            }            
        }
    }
}