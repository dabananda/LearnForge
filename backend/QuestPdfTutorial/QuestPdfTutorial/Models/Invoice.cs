namespace QuestPdfTutorial.Models
{
    public class Invoice
    {
        public Guid InvoiceNumber { get; set; }
        public string CustomerName { get; set; }
        public string CustomerAddress { get; set; }
        public DateTime InvoiceDate { get; set; }
        public IEnumerable<InvoiceItem> Items { get; set; } = Enumerable.Empty<InvoiceItem>();
        public decimal TotalAmount => Items.Sum(x => x.Total);
    }
}
