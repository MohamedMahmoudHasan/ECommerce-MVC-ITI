namespace ECommerce.DAL
{
    public class Product : IAuditableEntity
    {
        public int Id { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int Count { get; set; }
        public DateTime? ExpiryDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public string? ImageUrl { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public virtual Category Category { get; set; } =default!;


    }
}
