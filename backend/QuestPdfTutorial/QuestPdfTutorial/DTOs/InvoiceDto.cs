namespace QuestPdfTutorial.DTOs
{
    public class InvoiceDto
    {
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public IEnumerable<InvoiceItemDto> Items { get; set; }
    }
}
