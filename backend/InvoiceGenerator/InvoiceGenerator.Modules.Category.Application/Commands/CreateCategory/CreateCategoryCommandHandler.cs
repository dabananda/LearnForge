using InvoiceGenerator.Modules.Category.Application.Contracts;
using InvoiceGenerator.Shared.Common;
using MediatR;

namespace InvoiceGenerator.Modules.Category.Application.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository) : IRequestHandler<CreateCategoryCommand, Result>
    {
        public async Task<Result> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            if (request.ParentCategoryId is not null)
            {
                var parentCategory = await categoryRepository.GetByIdAsync(request.ParentCategoryId ?? Guid.Empty);
                if (parentCategory is null)
                    return Result.Failure();
            }

            var category = new Domain.Category(request.Name, request.Description, request.ParentCategoryId);
            await categoryRepository.CreateAsync(category);

            return Result.Success();
        }
    }
}
