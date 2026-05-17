using Dapper;
using LearnForge.Server.Api.Data.Repositories.Interfaces;
using LearnForge.Server.Api.Models.DomainModels;

namespace LearnForge.Server.Api.Data.Repositories
{
    public class UserRepository(IDapperContext context) : IUserRepository
    {
        public async Task<bool> CreateUserAsync(User user)
        {
            using var connection = context.CreateConnection();
            var query = @"INSERT INTO Users (Name, Email, Username, Gender, DateOfBirth)
                          VALUES (@Name, @Email, @Username, @Gender, @DateOfBirth)";
            return await connection.ExecuteAsync(query, user) > 0;
        }

        public async Task<bool> IsEmailUniqueAsync(string email)
        {
            using var connection = context.CreateConnection();
            var query = @"SELECT COUNT(*) FROM Users WHERE Email = @Email";
            var count = await connection.ExecuteScalarAsync<int>(query, new { Email = email });
            return count == 0;
        }
    }
}
