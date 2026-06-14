using FluentValidation;

namespace InvoiceGenerator.Modules.Category.Application.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(c => c.Name)
                .NotEmpty().WithMessage("Category name is required.")
                .MaximumLength(100).WithMessage("Category name must not exceed 100 characters.");

            RuleFor(c => c.Description)
                .MaximumLength(500).WithMessage("Description must not exceed 500 characters.");

            RuleFor(c => c.ParentCategoryId)
                .Must(id => id == null || id != Guid.Empty).WithMessage("Parent category ID must be a valid GUID.");
        }
    }
}
