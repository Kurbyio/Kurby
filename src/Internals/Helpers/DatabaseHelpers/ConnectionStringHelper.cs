using Microsoft.Extensions.Configuration;

namespace Kurby.Internals.Helpers.DatabaseHelper
{
    public class ConnectionStringHelper
    {
        
        public string CreateConnectionString(IConfiguration configuration)
        {
            var host = configuration.GetSection("Kurby:Database:DatabaseHost");
            var name = configuration.GetSection("Kurby:Database:DatabaseName");
            var user = configuration.GetSection("Kurby:Database:DatabaseUser");
            var password = configuration.GetSection("Kurby:Database:DatabasePass");

            return "Server=" + host.Value + ";Database=" + name.Value + ";User Id=" + user.Value + ";Password=" + password.Value;
        }
    }
}