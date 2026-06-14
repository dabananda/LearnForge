namespace InvoiceGenerator.Modules.Category.Application.Contracts
{
    public interface ICategoryRepository
    {
        Task<IQueryable<Domain.Category>> GetAllAsync();
    }
}
