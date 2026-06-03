using ECommerce.DAL;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL
{
    public class ProductManager : IProductManager
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductManager(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ProductReadMV> GetProducts()
        {
            var product = _unitOfWork.ProductRepository.GetAllWithCategory();
            var productReadMV = product.Select((e, i) => new ProductReadMV
            {
                Id = e.Id,
                Counter = i + 1,
                Title = e.Title,
                Price = e.Price,
                Description = e.Description,
                ExpiryDate = e.ExpiryDate,
                ImageURL = e.ImageUrl,
                Category = e.Category.Name
            }).ToList();
            return productReadMV;
        }

        public ProductReadMV? GetProductById(int productId)
        {
            var product = _unitOfWork.ProductRepository.GetByIdWithCategory(productId);
            if (product == null) return null;
            var productReadMV = new ProductReadMV
            {
                Id = product.Id,
                Title = product.Title,
                Price = product.Price,
                Description = product.Description,
                ExpiryDate = product.ExpiryDate,
                ImageURL = product.ImageUrl,
                Category = product.Category.Name

            };
            return productReadMV;
        }

        public ProductCreateMV? GetProductCreateMV()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            var categoryReadMV = categories.Select(e => new CategoryReadMV
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();
            var productCreateMV = new ProductCreateMV
            {
                Category = categoryReadMV
            };
            return productCreateMV;
        }

        public void CreateProduct(ProductCreateMV productCreateMV)
        {
            string? uniqueFileName = null;
            if (productCreateMV.Image != null)
            {
                uniqueFileName = SaveImageFile(productCreateMV.Image);
            }
            var product = new Product
            {
                Title = productCreateMV.Title,
                Description = productCreateMV.Description,
                Price = productCreateMV.Price,
                Count = productCreateMV.Count,
                ExpiryDate = productCreateMV.ExpiryDate,
                ImageUrl = uniqueFileName,
                CategoryId = productCreateMV.CategoryId
            };
            _unitOfWork.ProductRepository.Add(product);
            _unitOfWork.Save();
        }

        private string SaveImageFile(IFormFile image)
        {
            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Products");

            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            string filePath = Path.Combine(folderPath, uniqueFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                image.CopyTo(stream);
            }

            return uniqueFileName;
        }

        public void DeleteImageFile(string fileName)
        {
            string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", "Products");
            string filePath = Path.Combine(folderPath, fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        public bool GetProductsTitle(string title, int? id)
        {
            return _unitOfWork.ProductRepository.GetByTitle(title, id);
        }

        public void DeleteProduct(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    DeleteImageFile(product.ImageUrl);
                }
                _unitOfWork.ProductRepository.Delete(product);
                _unitOfWork.Save();
            }
        }

        public ProductEditMV? GetProductEditMV(int id)
        {
            var productEditMV = _unitOfWork.ProductRepository.GetByIdWithCategory(id);
            if (productEditMV == null) return null;
            var categoryReadMV = _unitOfWork.CategoryRepository.GetAll().Select(e => new CategoryReadMV
            {
                Id = e.Id,
                Name = e.Name
            }).ToList();

            var productEdit = new ProductEditMV
            {
                Id = productEditMV.Id,
                Title = productEditMV.Title,
                Description = productEditMV.Description,
                Price = productEditMV.Price,
                Count = productEditMV.Count,
                ExpiryDate = productEditMV.ExpiryDate,
                CurrentImageUrl = productEditMV.ImageUrl,
                CategoryId = productEditMV.CategoryId,
                Category = categoryReadMV
            };
            return productEdit;
        }

        public void EditProduct(ProductEditMV productEditMV)
        {
            var product = _unitOfWork.ProductRepository.GetById(productEditMV.Id);
            if (product == null) return;

            product.Title = productEditMV.Title;
            product.Description = productEditMV.Description;
            product.Price = productEditMV.Price;
            product.Count = productEditMV.Count;
            product.ExpiryDate = productEditMV.ExpiryDate;
            product.CategoryId = productEditMV.CategoryId;
            if (productEditMV.NewImage != null)
            {
                if (!string.IsNullOrEmpty(product.ImageUrl))
                {
                    DeleteImageFile(product.ImageUrl);
                }
                product.ImageUrl = SaveImageFile(productEditMV.NewImage);
            }
            _unitOfWork.Save();
        }

        public void DeleteProductImage(int id)
        {
            var product = _unitOfWork.ProductRepository.GetById(id);
            if (product != null && !string.IsNullOrEmpty(product.ImageUrl))
            {
                DeleteImageFile(product.ImageUrl);
                product.ImageUrl = null;
                _unitOfWork.Save();
            }
            return;
        }

        public IEnumerable<ProductReadMV> GetProductByCategory(int? id)
        {
            var prod = ((id == null)
            ? _unitOfWork.ProductRepository.GetAll()
            : _unitOfWork.ProductRepository.GetByCategory(id.Value)) ?? new List<Product>();

            var products = prod.Select((p, i) => new ProductReadMV
            {
                Id = p.Id,
                Counter = i + 1,
                Title = p.Title ?? "No Title",
                Description = p.Description ?? "No Description",
                Price = p.Price,
                Count = p.Count,
                ExpiryDate = p.ExpiryDate,
                ImageURL = p.ImageUrl,
                Category = p.Category?.Name ?? "No Category"
            }).ToList();

            return products;

        }

        public IEnumerable<CategoryReadMV> GetCategories()
        {
            var categories = _unitOfWork.CategoryRepository.GetAll();
            var categoryReadMVs = categories.Select(c => new CategoryReadMV
            {
                Id = c.Id,
                Name = c.Name
            }).ToList();
            return categoryReadMVs;
        }
    }
}
