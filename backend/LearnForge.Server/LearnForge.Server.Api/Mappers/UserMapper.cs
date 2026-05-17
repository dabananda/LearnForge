using LearnForge.Server.Api.DTOs;
using LearnForge.Server.Api.Features.Users.Commands.CreateUserCommand;
using LearnForge.Server.Api.Models.DomainModels;

namespace LearnForge.Server.Api.Mappers
{
    public static class UserMapper
    {
        public static User ToModel(this CreateUserCommand command)
        {
            return new User
            {
                Name = command.Name,
                Email = command.Email,
                Username = command.Email,
                Gender = command.Gender,
                DateOfBirth = command.DateOfBirth
            };
        }

        public static UserDto ToDto(this User user)
        {
            return new UserDto
            {
                Name = user.Name,
                Email = user.Email,
                Username = user.Username,
                Gender = user.Gender,
                DateOfBirth = user.DateOfBirth
            };
        }
    }
}
