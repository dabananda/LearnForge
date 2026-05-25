using QuestPdfTutorial.DTOs;

namespace QuestPdfTutorial.Services
{
    public interface IPdfService
    {
        byte[] GenerateInvoicePdf(InvoiceDto dto);
    }
}
