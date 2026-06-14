using Dapper;
using InvoiceGenerator.Modules.Category.Application.Contracts;

namespace InvoiceGenerator.Modules.Category.Infrastructure
{
    public class CategoryRepository(ISqlConnectionFactory context) : ICategoryRepository
    {
        public async Task<IQueryable<Domain.Category>> GetAllAsync()
        {
            using(var connection = context.CreateConnection())
            {
                const string query = "SELECT * FROM Categories";
                var categories = await connection.QueryAsync<Domain.Category>(query);
                return await Task.FromResult(categories.AsQueryable());
            }
        }
    }
}
