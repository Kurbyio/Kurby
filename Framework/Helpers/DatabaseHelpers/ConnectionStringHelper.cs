using Microsoft.Extensions.Configuration;

namespace Kurby.Framework.Helpers.DatabaseHelper
{
    public class ConnectionStringHelper
    {
        
        public string CreateConnectionString(IConfiguration configuration)
        {
            var host = configuration.GetSection("Database:DatabaseHost");
            var name = configuration.GetSection("Database:DatabaseName");
            var user = configuration.GetSection("Database:DatabaseUser");
            var password = configuration.GetSection("Database:DatabasePass");

            return "Server=" + host.Value + ";Database=" + name.Value + ";User Id=" + user.Value + ";Password=" + password.Value;
        }
    }
}