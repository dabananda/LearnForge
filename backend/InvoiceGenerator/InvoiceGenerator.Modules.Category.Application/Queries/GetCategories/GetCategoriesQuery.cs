using InvoiceGenerator.Modules.Category.Application.DTOs;
using InvoiceGenerator.Shared.Common;
using MediatR;

namespace InvoiceGenerator.Modules.Category.Application.Queries.GetCategories
{
    public class GetCategoriesQuery : IRequest<Result<PagedList<CategoryDto>>>
    {
        public string? SearchTerm { get; set; }
        public int? PageNumber { get; set; } = 1;
        public int? PageSize { get; set; } = 10;
        public bool? IsDescending { get; set; } = true;
    }
}
