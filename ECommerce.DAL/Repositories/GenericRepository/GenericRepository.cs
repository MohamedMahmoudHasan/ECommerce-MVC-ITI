

namespace ECommerce.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly EComAppDbContext _context;

        public GenericRepository(EComAppDbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void Delete(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }
    }
}
