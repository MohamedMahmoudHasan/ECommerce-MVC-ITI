using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL
{
    public interface ICategoryManager
    {
        IEnumerable<CategoryReadMV> GetAllCategories();

        CategoryReadMV? GetCategoryById(int id);

        CategoryEditMV? GetCategoryByIdForEdit(int id);

        void Insert(CategoryCreateMV categoryCreateMV);

        void Delete(int id);

        void Update(CategoryEditMV categoryEditMV);
    }
}
