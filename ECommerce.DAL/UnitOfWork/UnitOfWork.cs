using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly EComAppDbContext _context;
        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(EComAppDbContext context
            , ICategoryRepository categoryRepository
            , IProductRepository productRepository)
        {
            CategoryRepository = categoryRepository;
            ProductRepository = productRepository;
            _context = context;
        }


        public void Save()
        {
            _context.SaveChanges();
        }
    }
}
