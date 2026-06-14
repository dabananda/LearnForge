using InvoiceGenerator.Shared.Common;
using Microsoft.AspNetCore.Mvc;

namespace InvoiceGenerator.Shared
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        public IActionResult Handle<T>(Result<T> result)
        {
            return result switch
            {
                { IsFailure: true, ExceptionType: ExceptionType.AlreadyExists } => Conflict(result),
                { IsFailure: true, ExceptionType: ExceptionType.Forbidden } => Forbid(),
                { IsFailure: true, ExceptionType: ExceptionType.NotFound } => NotFound(result),
                { IsFailure: true, ExceptionType: ExceptionType.Validation } => BadRequest(result),
                { IsFailure: true, ExceptionType: ExceptionType.Unauthorized } => Unauthorized(),
                { IsFailure: true, ExceptionType: ExceptionType.InternalServerError } => StatusCode(500, result),
                { IsFailure: true } => BadRequest(result),
                _ => Ok(result)
            };
        }
    }
}
