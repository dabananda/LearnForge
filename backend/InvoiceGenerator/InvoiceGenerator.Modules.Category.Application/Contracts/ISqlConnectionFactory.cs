using System.Data;

namespace InvoiceGenerator.Modules.Category.Application.Contracts
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
