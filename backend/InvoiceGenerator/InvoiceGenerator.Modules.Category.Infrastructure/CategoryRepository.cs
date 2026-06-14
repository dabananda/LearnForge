using Dapper;
using InvoiceGenerator.Modules.Category.Application.Contracts;

namespace InvoiceGenerator.Modules.Category.Infrastructure
{
    public class CategoryRepository(ISqlConnectionFactory context) : ICategoryRepository
    {
        public async Task<IQueryable<Domain.Category>> GetAllAsync()
        {
            using (var connection = context.CreateConnection())
            {
                const string query = "SELECT * FROM Categories";
                var categories = await connection.QueryAsync<Domain.Category>(query);
                return await Task.FromResult(categories.AsQueryable());
            }
        }

        public async Task CreateAsync(Domain.Category category)
        {
            using (var connection = context.CreateConnection())
            {
                const string query = @"INSERT INTO Categories (Id, Name, Description, ParentCategoryId)
                                        VALUES (@Id, @Name, @Description, @ParentCategoryId)";
                await connection.ExecuteAsync(query, new { category.Id, category.Name, category.Description, category.ParentCategoryId });
            }
        }

        public async Task<Domain.Category?> GetByIdAsync(Guid id)
        {
            using (var connection = context.CreateConnection())
            {
                const string query = "SELECT * FROM Categories WHERE Id = @Id";
                return await connection.QueryFirstOrDefaultAsync<Domain.Category>(query, new { Id = id });
            }
        }
    }
}
