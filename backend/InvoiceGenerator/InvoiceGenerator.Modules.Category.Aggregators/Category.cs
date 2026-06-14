namespace InvoiceGenerator.Modules.Category.Aggregators
{
    public class Category
    {
        public Guid Id { get; private set; }
        public string Name { get; private set; }
        public string? Description { get; private set; }
        public bool IsActive { get; set; } = true;
        public Guid? ParentCategoryId { get; private set; }
        private readonly List<Category> _children = [];
        public IReadOnlyCollection<Category> Children => _children.AsReadOnly();

        public Category(string name, string? description = null, Guid? parentCategoryId = null)
        {
            Id = Guid.NewGuid();
            Name = name;
            Description = description;
            ParentCategoryId = parentCategoryId;
        }

        public void DeActivate()
        {
            IsActive = false;
        }

        public void ReActivate()
        {
            IsActive = true;
        }

        public void Rename(string newName)
        {
            Name = newName;
        }
    }
}
