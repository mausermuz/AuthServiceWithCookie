using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace AuthServiceBulgakov.DataAccess.MSSQL
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("DbConnection");
        }

        public IDbConnection CreateConnetion()
            => new SqlConnection(_connectionString);
    }
}
