using FluentValidation;

namespace InvoiceGenerator.Modules.Category.Application.Queries.GetById
{
    public class GetByIdQueryValidator : AbstractValidator<GetByIdQuery>
    {
        public GetByIdQueryValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Id is required.")
                .Must(id => id == null || id != Guid.Empty).WithMessage("ID must be a valid GUID.");
        }
    }
}
