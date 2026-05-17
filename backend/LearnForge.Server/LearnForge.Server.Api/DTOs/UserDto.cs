using LearnForge.Server.Api.Models.Enums;

namespace LearnForge.Server.Api.DTOs
{
    public class UserDto
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
