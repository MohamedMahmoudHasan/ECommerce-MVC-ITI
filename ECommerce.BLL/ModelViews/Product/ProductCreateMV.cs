using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
namespace ECommerce.BLL
{
    public class ProductCreateMV
    {
        [Display(Name = "Product Title")]
        [Remote(action: "IsTitleExist", controller: "Product")]
        public string Title { get; set; } = default!;
        [Required]
        [MinLength(10)]
        public string Description { get; set; } = default!;
        [Required]
        [Range(10, 30000, ErrorMessage = "Price must be greater than 10")]
        public decimal Price { get; set; }
        [Required]
        public int Count { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }

        [Required]
        public IFormFile? Image { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public List<CategoryReadMV> Category { get; set; } = new List<CategoryReadMV>();
    }
}
