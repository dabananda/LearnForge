using InvoiceGenerator.Modules.Category.Application;
using InvoiceGenerator.Modules.Category.Infrastructure;

namespace InvoiceGenerator.Modules.Category.Api
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCategoryModule(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure();

            return services;
        }
    }
}
