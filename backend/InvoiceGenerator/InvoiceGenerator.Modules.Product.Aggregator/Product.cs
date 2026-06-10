namespace InvoiceGenerator.Modules.Product.Aggregator
{
    public class Product
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public decimal UnitPrice { get; private set; }
        public int Quantity { get; private set; }

        public Product(string name, decimal unitPrice)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ArgumentException("Product name cannot be null or empty.", nameof(name));

            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

            Id = Guid.NewGuid();
            Name = name;
            UnitPrice = unitPrice;
        }

        public void AddQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            if (Quantity < quantity)
                throw new InvalidOperationException("Insufficient quantity.");

            Quantity += quantity;
        }

        public void RemoveQuantity(int quantity)
        {
            if (quantity <= 0)
                throw new ArgumentOutOfRangeException(nameof(quantity));

            if (Quantity < quantity)
                throw new InvalidOperationException("Insufficient quantity.");

            Quantity -= quantity;
        }

        public void UpdateUnitPrice(decimal unitPrice)
        {
            if (unitPrice < 0)
                throw new ArgumentOutOfRangeException(nameof(unitPrice), "Unit price cannot be negative.");

            UnitPrice = unitPrice;
        }
    }
}
