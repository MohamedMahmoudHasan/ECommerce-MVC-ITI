using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.BLL
{
    public interface IProductManager
    {
        ProductReadMV? GetProductById(int productId);
        IEnumerable<ProductReadMV> GetProducts();
        bool GetProductsTitle(string title, int? id);

        ProductCreateMV? GetProductCreateMV();
        void CreateProduct(ProductCreateMV productCreateMV);
        ProductEditMV? GetProductEditMV(int id);
        void EditProduct(ProductEditMV productEditMV);
        void DeleteProduct(int id);
        void DeleteProductImage(int id);
        IEnumerable<ProductReadMV> GetProductByCategory(int? id);
        IEnumerable<CategoryReadMV> GetCategories();
    }
}
