using LearnForge.Server.Api.Models.DomainModels;

namespace LearnForge.Server.Api.Data.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<bool> IsEmailUniqueAsync(string email);
        Task<bool> CreateUserAsync(User user);
    }
}
