using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using vancil.Framework.Helpers.DatabaseHelper;
using vancil.Models;

namespace vancil.App.Config
{
    public class Database
    {
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