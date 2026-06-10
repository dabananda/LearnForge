using Dapper;
using InvoiceGenerator.Modules.Product.Repositories.Abstractions;

namespace InvoiceGenerator.Modules.Product.Repositories
{
    public class ProductRepository(ISqlConnectionFactory _context) : IProductRepository
    {
        public Task AddProduct(Aggregator.Product product)
        {
            using var connection = _context.CreateConnection();
            const string query = "INSERT INTO Products (Id, Name, Price) VALUES (@Id, @Name, @Price)";
            return connection.ExecuteAsync(query, new { product.Id, product.Name, product.UnitPrice });
        }

        public Task DeleteProduct(Aggregator.Product product)
        {
            using var connection = _context.CreateConnection();
            const string query = "DELETE FROM Products WHERE Id = @Id";
            return connection.ExecuteAsync(query, new { Id = product.Id });
        }

        public async Task<Aggregator.Product?> GetProductByIdAsync(Guid id)
        {
            using var connection = _context.CreateConnection();
            const string query = "SELECT * FROM Products WHERE Id = @Id";
            return await connection.QueryFirstOrDefaultAsync<Aggregator.Product>(query, new { Id = id });
        }

        public Task<IEnumerable<Aggregator.Product>> GetProductsAsync()
        {
            using var connection = _context.CreateConnection();
            const string query = "SELECT * FROM Products";
            return connection.QueryAsync<Aggregator.Product>(query);
        }

        public Task UpdateProduct(Aggregator.Product product)
        {
            using var connection = _context.CreateConnection();
            const string query = "UPDATE Products SET Name = @Name, Price = @Price WHERE Id = @Id";
            return connection.ExecuteAsync(query, new { product.Id, product.Name, product.UnitPrice });
        }
    }
}
