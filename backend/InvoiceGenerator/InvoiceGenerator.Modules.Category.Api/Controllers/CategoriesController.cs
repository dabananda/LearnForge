using InvoiceGenerator.Modules.Category.Application.Commands.CreateCategory;
using InvoiceGenerator.Modules.Category.Application.Queries.GetById;
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

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCategoryCommand command)
        {
            return Handle(await mediator.Send(command));
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById([FromRoute] Guid id)
        {
            return Handle(await mediator.Send(new GetByIdQuery { Id = id }));
        }
    }
}