namespace LearnForge.Server.Api.Services.Interfaces
{
    public interface IPasswordHasher
    {
        Task<string> HashPasswordAsync(string password);
        Task<bool> VerifyPasswordAsync(string hashedPassword, string password);
    }
}
