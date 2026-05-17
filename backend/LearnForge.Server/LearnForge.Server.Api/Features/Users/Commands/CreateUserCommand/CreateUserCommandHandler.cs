using LearnForge.Server.Api.Data.Repositories.Interfaces;
using LearnForge.Server.Api.DTOs;
using LearnForge.Server.Api.Mappers;
using MediatR;

namespace LearnForge.Server.Api.Features.Users.Commands.CreateUserCommand
{
    public class CreateUserCommandHandler(IUserRepository userRepository) : IRequestHandler<CreateUserCommand, Result>
    {
        public async Task<Result> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isEmailUnique = await userRepository.IsEmailUniqueAsync(request.Email);
            
            if (isEmailUnique is false)
                return Result.Failure("Email already exists");

            var user = UserMapper.ToModel(request);
            var result = await userRepository.CreateUserAsync(user);

            return result ? Result.Success("User created successfully.") : Result.Failure("Failed to create user.");
        }
    }
}
