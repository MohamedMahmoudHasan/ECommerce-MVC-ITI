
namespace ECommerce.DAL

{
    public class CategoryRepository : GenericRepository<Category> , ICategoryRepository
    {
        public CategoryRepository(EComAppDbContext context) : base(context)
        {
        }


        //public IEnumerable<Category> GetAll()
        //{
        //     return _context.Categories.ToList();
        //}

        //public Category? GetById(int categoryId)
        //{
        //    return _context.Categories.FirstOrDefault(e => e.Id == categoryId);
        //}

        public void SaveChanges()
        {
            _context.SaveChanges();
        }

    }
}
