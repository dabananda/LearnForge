using Microsoft.AspNetCore.Mvc;
using QuestPdfTutorial.DTOs;
using QuestPdfTutorial.Services;

namespace QuestPdfTutorial.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PdfController(IPdfService pdfService) : ControllerBase
    {
        [HttpPost("invoice")]
        public IActionResult GetInvoicePdf([FromBody] InvoiceDto dto)
        {
            var pdfBytes = pdfService.GenerateInvoicePdf(dto);
            return File(pdfBytes, "application/pdf", "Invoice.pdf");
        }
    }
}
