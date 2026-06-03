
namespace ECommerce.DAL
{
    public class Category: IAuditableEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public virtual ICollection<Product> Products { get; set; } = new List<Product>();
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
