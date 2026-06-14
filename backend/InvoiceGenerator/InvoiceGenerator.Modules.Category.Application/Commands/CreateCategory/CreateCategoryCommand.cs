using InvoiceGenerator.Shared.Common;
using MediatR;

namespace InvoiceGenerator.Modules.Category.Application.Commands.CreateCategory
{
    public class CreateCategoryCommand : IRequest<Result>
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public Guid? ParentCategoryId { get; set; }
    }
}
