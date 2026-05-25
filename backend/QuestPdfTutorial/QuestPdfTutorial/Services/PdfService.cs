using QuestPDF.Fluent;
using QuestPdfTutorial.Documents;
using QuestPdfTutorial.DTOs;
using QuestPdfTutorial.Models;

namespace QuestPdfTutorial.Services
{
    public class PdfService : IPdfService
    {
        public byte[] GenerateInvoicePdf(InvoiceDto dto)
        {
            var invoice = new Invoice
            {
                InvoiceNumber = Guid.NewGuid(),
                CustomerName = dto.CustomerName,
                CustomerAddress = dto.CustomerAddress,
                InvoiceDate = DateTime.UtcNow,
                Items = dto.Items.Select(i => new InvoiceItem
                {
                    InvoiceItemId = Guid.NewGuid(),
                    ProductName = i.ProductName,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice
                })
            };
            var document = new InvoiceDocument(invoice);
            return document.GeneratePdf();
        }
    }
}
