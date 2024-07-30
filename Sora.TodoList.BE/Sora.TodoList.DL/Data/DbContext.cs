using Microsoft.Extensions.Configuration;
using MySqlConnector;
using System.Data.Common;

namespace Sora.TodoList.DL.Data
{
    public class DbContext
    {
        private const string ConnectionStringName = "MySql";

        private readonly IConfiguration _configuration;

        public DbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbConnection GetConnection()
        {
            return new MySqlConnection(_configuration.GetConnectionString(ConnectionStringName));
        }
    }
}