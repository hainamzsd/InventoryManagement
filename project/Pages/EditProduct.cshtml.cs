using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{

    public class EditProductModel : PageModel
    {

        private ProductManager _productManager;
        private CategoryManager _categoryManager;
        private SupplierManager _supplierManager;

        [BindProperty]
        public Product product { get; set; }

        public List<Supplier> supplier { get; set; }

        public List<Category> category { get; set; }  

        public EditProductModel(ProductManager productManager
            , CategoryManager categoryManager, SupplierManager supplierManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _supplierManager = supplierManager;
        }

        public IActionResult OnGet(int productId)
        {

            product = _productManager.GetProductById(productId);
            if(product == null)
            {
                return NotFound();
            }
            supplier = (List<Supplier>)_supplierManager.GetSuppliers();
            category = (List<Category>)_categoryManager.GetCategorys();
            return Page();
        }

        public IActionResult OnPostEditProduct()
        {

            var existingProduct = _productManager.GetProductById(product.ProductId);

            if (existingProduct == null)
            {
                return NotFound();
            }

            // Update the existing product with the posted form values

            existingProduct.ProductName = string.IsNullOrEmpty(product.ProductName) ? existingProduct.ProductName : product.ProductName;
            existingProduct.SupplierId = product.SupplierId ?? existingProduct.SupplierId;
            existingProduct.CategoryId = product.CategoryId ?? existingProduct.CategoryId;
            existingProduct.QuantityPerUnit = string.IsNullOrEmpty(product.QuantityPerUnit) ? existingProduct.QuantityPerUnit : product.QuantityPerUnit;
            existingProduct.UnitPrice = product.UnitPrice ?? existingProduct.UnitPrice;
            existingProduct.UnitsInStock = product.UnitsInStock ?? existingProduct.UnitsInStock;
            existingProduct.UnitsOnOrder = product.UnitsOnOrder ?? existingProduct.UnitsOnOrder;
            existingProduct.ReorderLevel = product.ReorderLevel ?? existingProduct.ReorderLevel;
            existingProduct.Discontinued = product.Discontinued;
            _productManager.UpdateProduct(existingProduct);
            supplier = (List<Supplier>)_supplierManager.GetSuppliers();
            category = (List<Category>)_categoryManager.GetCategorys();
            return Page();
        }
    }
}
