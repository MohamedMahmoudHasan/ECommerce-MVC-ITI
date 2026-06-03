using ECommerce.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL
{
    public class CategoryManager : ICategoryManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<CategoryReadMV> GetAllCategories()
        {
            var categoriesMV = _unitOfWork.CategoryRepository.GetAll()
                .Select((c,i) => new CategoryReadMV
                {
                    Id = c.Id,
                    Name = c.Name,
                    Counter = i+1
                });
            return categoriesMV;
        }

        public CategoryReadMV? GetCategoryById(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return null;
            
            var categoriesMV = new CategoryReadMV{
                Id = category.Id,
                Name = category.Name,
            };
            return categoriesMV;
        }

        public void Insert(CategoryCreateMV categoryCreateMV)
        {
            var category = new Category
            {
                Name = categoryCreateMV.Name,
            };

            _unitOfWork.CategoryRepository.Add(category);
            _unitOfWork.Save();
        }

        public void Delete(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return;
            _unitOfWork.CategoryRepository.Delete(category);
            _unitOfWork.Save();
        }

        public CategoryEditMV? GetCategoryByIdForEdit(int id)
        {
            var category = _unitOfWork.CategoryRepository.GetById(id);
            if (category == null)
                return null;

            var categoriesMV = new CategoryEditMV
            {
                Id = category.Id,
                Name = category.Name,
            };
            return categoriesMV;
        }

        public void Update(CategoryEditMV categoryEditMV)
        {
            var category = _unitOfWork.CategoryRepository.GetById(categoryEditMV.Id);
            if (category == null)
                return;

            category.Name = categoryEditMV.Name;

            _unitOfWork.Save();
        }
    }
}
