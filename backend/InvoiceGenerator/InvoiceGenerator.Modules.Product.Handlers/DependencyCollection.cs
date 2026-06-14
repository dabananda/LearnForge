using Microsoft.Extensions.DependencyInjection;

namespace InvoiceGenerator.Modules.Product.Handlers
{
    public static class DependencyCollection
    {
        public static IServiceCollection AddProduct(this IServiceCollection services)
        {
            
            return services;
        }
    }
}
