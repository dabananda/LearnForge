using System.Data;

namespace LearnForge.Server.Api.Data.Repositories.Interfaces
{
    public interface IDapperContext
    {
        IDbConnection CreateConnection();
    }
}
