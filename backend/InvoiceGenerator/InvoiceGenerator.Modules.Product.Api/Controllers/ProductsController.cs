using InvoiceGenerator.Modules.Product.DTOs.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceGenerator.Modules.Product.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController(IMediator mediator) : ControllerBase
    {
        [HttpGet("GetProductById/{id:guid}")]
        public async Task<IActionResult> GetProductById(Guid id)
        {
            var result = await mediator.Send(new GetProductByIdQuery { Id = id });
            return Ok(result);
        }
    }
}