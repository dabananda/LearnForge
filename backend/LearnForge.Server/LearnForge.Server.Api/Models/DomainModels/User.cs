using LearnForge.Server.Api.Models.Enums;

namespace LearnForge.Server.Api.Models.DomainModels
{
    public class User
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Username { get; set; }
        public required Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
