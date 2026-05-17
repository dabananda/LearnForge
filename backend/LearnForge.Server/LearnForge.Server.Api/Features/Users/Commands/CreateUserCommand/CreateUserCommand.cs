using LearnForge.Server.Api.DTOs;
using LearnForge.Server.Api.Models.Enums;
using MediatR;

namespace LearnForge.Server.Api.Features.Users.Commands.CreateUserCommand
{
    public class CreateUserCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public Gender Gender { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
