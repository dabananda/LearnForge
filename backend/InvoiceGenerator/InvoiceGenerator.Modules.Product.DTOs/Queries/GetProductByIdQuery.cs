using InvoiceGenerator.Modules.Product.DTOs.DTOs;
using MediatR;

namespace InvoiceGenerator.Modules.Product.DTOs.Queries
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public Guid Id { get; set; }
    }
}
