using InvoiceGenerator.Modules.Category.Application.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace InvoiceGenerator.Modules.Category.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCategory(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DependencyInjection).Assembly));

            services.AddScoped<ISqlConnectionFactory, SqlConnectionFactory>();

            services.AddScoped<ICategoryRepository, CategoryRepository>();

            return services;
        }
    }
}
