using InvoiceGenerator.Modules.Product.DTOs.DTOs;
using InvoiceGenerator.Modules.Product.DTOs.Queries;
using InvoiceGenerator.Modules.Product.Repositories.Abstractions;
using MediatR;

namespace InvoiceGenerator.Modules.Product.Handlers.Queries
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository) : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetProductByIdAsync(request.Id);
            if (product == null)
                throw new InvalidOperationException("Product not found");

            var dto = Aggregator.Product.ToDTO(product);
            return dto;
        }
    }
}