namespace InvoiceGenerator.Modules.Category.Application.Contracts
{
    public interface ICategoryRepository
    {
        Task<IQueryable<Domain.Category>> GetAllAsync();
        Task CreateAsync(Domain.Category category);
        Task<Domain.Category?> GetByIdAsync(Guid id);
    }
}
