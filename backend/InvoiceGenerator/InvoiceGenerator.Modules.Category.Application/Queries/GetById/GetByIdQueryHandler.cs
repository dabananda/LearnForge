using InvoiceGenerator.Modules.Category.Application.Contracts;
using InvoiceGenerator.Modules.Category.Application.DTOs;
using InvoiceGenerator.Shared.Common;
using MediatR;

namespace InvoiceGenerator.Modules.Category.Application.Queries.GetById
{
    internal class GetByIdQueryHandler(ICategoryRepository categoryRepository) : IRequestHandler<GetByIdQuery, Result<CategoryDto>>
    {
        public async Task<Result<CategoryDto>> Handle(GetByIdQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.GetByIdAsync(request.Id);
            if (category is null)
                return Result<CategoryDto>.Failure();

            var categoryDto = new CategoryDto
            {
                Id = category.Id,
                Name = category.Name,
                Description = category.Description,
                ParentCategoryId = category.ParentCategoryId
            };

            return Result<CategoryDto>.Success(categoryDto);
        }
    }
}
