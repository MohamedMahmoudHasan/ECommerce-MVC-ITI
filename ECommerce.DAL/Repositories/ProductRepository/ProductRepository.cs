

using Microsoft.EntityFrameworkCore;

namespace ECommerce.DAL
{
    public class ProductRepository : GenericRepository<Product>,IProductRepository
    {
        public ProductRepository(EComAppDbContext context) : base(context)
        {
        }


        public IEnumerable<Product> GetAllWithCategory()
        {
            return _context.Products.Include(e => e.Category).ToList();
        }

        public IEnumerable<Product>? GetByCategory(int catId)
        {
            return _context.Products.Include(e => e.Category).Where(e=>e.CategoryId == catId).ToList();
        }

        public Product? GetByIdWithCategory(int productId)
        {
            return _context.Products.Include(e => e.Category).FirstOrDefault(e => e.Id == productId);
        }

        public bool GetByTitle(string title, int? id)
        {
            return _context.Products.Any(p => p.Title == title && p.Id != id);
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }

        
    }
}
