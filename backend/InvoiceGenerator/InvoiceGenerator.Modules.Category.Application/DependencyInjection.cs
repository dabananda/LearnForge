using FluentValidation;
using InvoiceGenerator.Modules.Category.Application.Behaviors;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceGenerator.Modules.Category.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddValidatorsFromAssembly(typeof(DependencyInjection).Assembly);

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

            return services;
        }
    }
}
