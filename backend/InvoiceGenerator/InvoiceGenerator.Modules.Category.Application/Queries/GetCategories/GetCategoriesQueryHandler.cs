using InvoiceGenerator.Modules.Category.Application.Contracts;
using InvoiceGenerator.Modules.Category.Application.DTOs;
using InvoiceGenerator.Shared.Common;
using MediatR;

namespace InvoiceGenerator.Modules.Category.Application.Queries.GetCategories
{
    public class GetCategoriesQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetCategoriesQuery, Result<PagedList<CategoryDto>>>
    {
        public async Task<Result<PagedList<CategoryDto>>> Handle(GetCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = await categoryRepository.GetAllAsync();

            if (!string.IsNullOrEmpty(request.SearchTerm))
                categories = categories.Where(c => c.Name.Contains(request.SearchTerm));

            if (request.IsDescending == true)
                categories = categories.OrderByDescending(c => c.Name);

            if (request.PageNumber.HasValue && request.PageSize.HasValue)
                categories = categories.Skip((request.PageNumber.Value - 1) * request.PageSize.Value).Take(request.PageSize.Value);

            var response = categories.Select(c => new CategoryDto
            {
                Id = c.Id,
                Name = c.Name,
                Description = c.Description,
                ParentCategoryId = c.ParentCategoryId
            }).ToList();

            var pagedResponse = new PagedList<CategoryDto>(response, request.PageNumber ?? 1, request.PageSize ?? 10, response.Count);
            
            return Result<PagedList<CategoryDto>>.Success(pagedResponse);
        }
    }
}
