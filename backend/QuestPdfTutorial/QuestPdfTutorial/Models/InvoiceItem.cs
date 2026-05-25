namespace QuestPdfTutorial.Models
{
    public class InvoiceItem
    {
        public Guid InvoiceItemId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal Total => Quantity * UnitPrice;
    }
}
