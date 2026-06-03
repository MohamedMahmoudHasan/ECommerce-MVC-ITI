
using ECommerce.BLL;
using ECommerce.DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.PL.Controllers
{
    [PerformanceFilter]
    [TypeFilter(typeof(LoggerFilter))]
    [Authorize]
    public class ProductController : Controller
    {

        private readonly IProductManager _productManager;

        public ProductController(IProductManager productManager)
        {
            _productManager = productManager;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var productsReadVM = _productManager.GetProducts();

            ViewBag.Categories = _productManager.GetCategories().Select((e, i) => new SelectListItem
            {
                Value = e.Id.ToString(),
                Text = e.Name
            });
            return View(productsReadVM);
        }

        [HttpGet]
        public IActionResult Details(int id)
        {
            var productReadVM = _productManager.GetProductById(id);
            return View(productReadVM);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            var category = _productManager.GetProductCreateMV();
            var productCreateMVPL = new ProductCreateMVPL
            {
                Category = category?.Category.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = c.Name
                }).ToList() ?? new List<SelectListItem>()
            };
            return View(productCreateMVPL);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Create(ProductCreateMVPL product)
        {
            if (!ModelState.IsValid)
            {
                var category = _productManager.GetProductCreateMV();
                var productCreateMVPL = new ProductCreateMVPL
                {
                    Category = category?.Category.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList() ?? new List<SelectListItem>()
                };
                return View(productCreateMVPL);
            }

            var productCreateMV = new ProductCreateMV
            {
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Count = product.Count,
                ExpiryDate = product.ExpiryDate,
                Image = product.Image,
                CategoryId = product.CategoryId
            };

            _productManager.CreateProduct(productCreateMV);


            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            var productEditMV = _productManager.GetProductEditMV(id);

                if (productEditMV == null)
                {
                    return NotFound();
                }
    
                var productEditMVPL = new ProductEditMVPL
                {
                    Id = productEditMV.Id,
                    Title = productEditMV.Title,
                    Description = productEditMV.Description,
                    Price = productEditMV.Price,
                    Count = productEditMV.Count,
                    ExpiryDate = productEditMV.ExpiryDate,
                    CurrentImageUrl = productEditMV.CurrentImageUrl,
                    CategoryId = productEditMV.CategoryId,
                    Category = productEditMV.Category.Select(c => new SelectListItem
                    {
                        Value = c.Id.ToString(),
                        Text = c.Name
                    }).ToList()
                };

            return View(productEditMVPL);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public IActionResult Edit(ProductEditMVPL product)
        {
            
            if (!ModelState.IsValid)
            {
                var productEditMVPL = new ProductEditMVPL
                {
                    Id = product.Id,
                    Title = product.Title,
                    Description = product.Description,
                    Price = product.Price,
                    Count = product.Count,
                    ExpiryDate = product.ExpiryDate,
                    CurrentImageUrl = product.CurrentImageUrl,
                    CategoryId = product.CategoryId,
                    Category = product.Category
                };
                return View(product);
            }
            var productEditMV = new ProductEditMV
            {
                Id = product.Id,
                Title = product.Title,
                Description = product.Description,
                Price = product.Price,
                Count = product.Count,
                ExpiryDate = product.ExpiryDate,
                NewImage = product.NewImage,
                CurrentImageUrl = product.CurrentImageUrl,
                CategoryId = product.CategoryId,
            };
            _productManager.EditProduct(productEditMV);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            _productManager.DeleteProduct(id);

            return RedirectToAction(nameof(Index));
        }



        [AcceptVerbs("Get", "Post")]
        [Authorize(Roles = "Admin")]
        public IActionResult IsTitleExist(string title, int? id)
        {
            var isExist = _productManager.GetProductsTitle(title, id);
            if (isExist)
            {
                return Json($"Product with title '{title}' already exists.");
            }
            return Json(true);
        }

        [HttpGet]
        public IActionResult GetProductByCategory(int? id)
        {
            var products = _productManager.GetProductByCategory(id);

            return PartialView("_ProdCat", products);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteImage(int id)
        {
            _productManager.DeleteProductImage(id);

            return RedirectToAction(nameof(Edit), new { id });
        }



    }
}
