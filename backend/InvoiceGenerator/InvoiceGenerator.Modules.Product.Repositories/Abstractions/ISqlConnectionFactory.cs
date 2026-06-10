using System.Data;

namespace InvoiceGenerator.Modules.Product.Repositories.Abstractions
{
    public interface ISqlConnectionFactory
    {
        IDbConnection CreateConnection();
    }
}
