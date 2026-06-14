using InvoiceGenerator.Modules.Category.Application.Queries.GetCategories;
using InvoiceGenerator.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceGenerator.Modules.Category.Api.Controllers
{
    public class CategoriesController(IMediator mediator) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] GetCategoriesQuery query)
        {
            return Handle(await mediator.Send(query));
        }
    }
}
