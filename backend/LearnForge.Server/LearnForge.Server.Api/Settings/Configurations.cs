using FluentValidation;
using FluentValidation.AspNetCore;
using LearnForge.Server.Api.Data.Context;
using LearnForge.Server.Api.Data.Repositories;
using LearnForge.Server.Api.Data.Repositories.Interfaces;
using LearnForge.Server.Api.Features.Users.Commands.CreateUserCommand;
using LearnForge.Server.Api.Services;
using LearnForge.Server.Api.Services.Interfaces;

namespace LearnForge.Server.Api.Settings
{
    public static class Configurations
    {
        public static IServiceCollection AddConfigurations(this IServiceCollection services, IConfiguration configuration)
        {
            var licenseSettings = configuration.GetSection("LuckyPennyLicense").Get<LuckyPennyLicenseSettings>();
            
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.Configure<LuckyPennyLicenseSettings>(configuration.GetSection("LuckyPennyLicense"));
            services.Configure<ConnectionStringSettings>(configuration.GetSection("ConnectionStrings"));

            services.AddMediatR(cfg =>
            {
                cfg.LicenseKey = licenseSettings?.LicenseKey;
                cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
            });

            services.AddFluentValidationAutoValidation()
                    .AddFluentValidationClientsideAdapters()
                    .AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IUserRepository, UserRepository>();

            return services;
        }
    }
}
