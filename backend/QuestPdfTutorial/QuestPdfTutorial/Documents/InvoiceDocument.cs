using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using QuestPdfTutorial.Models;

namespace QuestPdfTutorial.Documents
{
    public class InvoiceDocument : IDocument
    {
        private readonly Invoice _invoice;

        public InvoiceDocument(Invoice invoice)
        {
            _invoice = invoice;
        }

        public DocumentMetadata GetMetadata()
        {
            return DocumentMetadata.Default;
        }

        public void Compose(IDocumentContainer container)
        {
            container.Page(page =>
            {
                page.Margin(40);
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().Text(x =>
                    x.Span("Generated with QuestPDF by Dabananda Mitra")
                    .Italic().FontSize(12));
            });
        }

        private void ComposeHeader(IContainer container)
        {
            container.Column(column =>
            {
                column.Item().Text("INVOICE").FontSize(28).Bold();
                column.Item().Text($"Invoice No: {_invoice.InvoiceNumber}");
                column.Item().Text($"Customer: {_invoice.CustomerName}");
                column.Item().Text($"Address: {_invoice.CustomerAddress}");
                column.Item().Text($"Date: {_invoice.InvoiceDate:dd MMM yyyy}");
            });
        }

        private void ComposeContent(IContainer container)
        {
            container.PaddingTop(25).Table(table =>
            {
                table.ColumnsDefinition(columns =>
                {
                    columns.RelativeColumn(4);
                    columns.RelativeColumn(1);
                    columns.RelativeColumn(2);
                    columns.RelativeColumn(2);
                });

                table.Header(header =>
                {
                    header.Cell().Element(CellStyle).Text("Product");
                    header.Cell().Element(CellStyle).AlignCenter().Text("Qty");
                    header.Cell().Element(CellStyle).AlignRight().Text("Price");
                    header.Cell().Element(CellStyle).AlignRight().Text("Total");
                });

                foreach (var item in _invoice.Items)
                {
                    table.Cell().Element(CellStyle).Text(item.ProductName);
                    table.Cell().Element(CellStyle).AlignCenter().Text(item.Quantity.ToString());
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.UnitPrice:C}");
                    table.Cell().Element(CellStyle).AlignRight().Text($"{item.Total:C}");
                }

                table.Cell().ColumnSpan(3).AlignRight().Text("Grand Total").Bold();
                table.Cell().AlignRight().Text($"{_invoice.TotalAmount:C}").Bold();
            });
        }

        private static IContainer CellStyle(IContainer container)
        {
            return container
                .BorderBottom(1)
                .BorderColor(Colors.Grey.Lighten2)
                .PaddingVertical(5)
                .PaddingHorizontal(5);
        }
    }
}