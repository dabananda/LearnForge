using InvoiceGenerator.Modules.Category.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceGenerator.Modules.Category.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
