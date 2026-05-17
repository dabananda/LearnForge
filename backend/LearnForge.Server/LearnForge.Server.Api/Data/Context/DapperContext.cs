using LearnForge.Server.Api.Data.Repositories.Interfaces;
using LearnForge.Server.Api.Settings;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using System.Data;

namespace LearnForge.Server.Api.Data.Context
{
    public class DapperContext(IOptions<ConnectionStringSettings> connectionStrings) : IDapperContext
    {
        private readonly string _defaultConnectionString = connectionStrings.Value.DefaultConnection;

        public IDbConnection CreateConnection() => new SqlConnection(_defaultConnectionString);
    }
}
