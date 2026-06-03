using System.ComponentModel.DataAnnotations;

namespace ECommerce.BLL
{
    public class ProductReadMV
    {
        public int Id { get; set; }
        public int Counter { get; set; }
        public string Title { get; set; } = default!;
        public string Description { get; set; } = default!;
        public decimal Price { get; set; }
        public int Count { get; set; }

        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        public string? ImageURL { get; set; }


        public string Category { get; set; } = default!;
    }
}
