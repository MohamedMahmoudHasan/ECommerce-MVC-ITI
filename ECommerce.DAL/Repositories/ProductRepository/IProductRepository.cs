

namespace ECommerce.DAL
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Product> GetAllWithCategory();
        Product? GetByIdWithCategory(int productId);
        IEnumerable<Product>? GetByCategory(int catId);
        bool GetByTitle(string title, int? id);
        void SaveChanges();
    }
}
