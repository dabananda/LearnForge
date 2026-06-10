namespace InvoiceGenerator.Modules.Product.Repositories.Abstractions
{
    public interface IProductRepository
    {
        Task<Aggregator.Product?> GetProductByIdAsync(Guid id);
        Task<IEnumerable<Aggregator.Product>> GetProductsAsync();
        Task AddProduct(Aggregator.Product product);
        Task UpdateProduct(Aggregator.Product product);
        Task DeleteProduct(Aggregator.Product product);
    }
}
