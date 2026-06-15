using InvoiceGenerator.Modules.Category.Application.DTOs;
using InvoiceGenerator.Shared.Common;
using MediatR;

namespace InvoiceGenerator.Modules.Category.Application.Queries.GetById
{
    public class GetByIdQuery : IRequest<Result<CategoryDto>>
    {
        public Guid Id { get; set; }
    }
}
