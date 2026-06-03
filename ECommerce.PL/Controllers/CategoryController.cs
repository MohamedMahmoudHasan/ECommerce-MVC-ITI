
using ECommerce.BLL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.PL.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        private readonly ICategoryManager _categoryManager;

        public CategoryController(ICategoryManager categoryManager)
        {
            _categoryManager = categoryManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var CategoryReadMV = _categoryManager.GetAllCategories();
            return View(CategoryReadMV);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var categoryMV = _categoryManager.GetCategoryById(id);            
            return View(categoryMV);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(CategoryCreateMV category)
        {
            
            _categoryManager.Insert(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var categoryMV = _categoryManager.GetCategoryByIdForEdit(id);

            return View(categoryMV);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(CategoryEditMV category)
        {
            _categoryManager.Update(category);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _categoryManager.Delete(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
