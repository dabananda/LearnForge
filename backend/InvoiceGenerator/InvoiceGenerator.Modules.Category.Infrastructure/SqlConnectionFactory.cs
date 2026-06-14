using InvoiceGenerator.Modules.Category.Application.Contracts;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace InvoiceGenerator.Modules.Category.Infrastructure
{
    public class SqlConnectionFactory(IConfiguration configuration) : ISqlConnectionFactory
    {
        private readonly string _connectionString = configuration.GetConnectionString("DefaultConnection")!;

        public IDbConnection CreateConnection()
        {
            return new SqlConnection(_connectionString);
        }
    }
}
