using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class AddProductModel : PageModel
    {
        public Product product { get; set; }
        public List<Supplier> supplier { get; set; }
        public List<Category> category { get; set; }

        private ProductManager _productManager;
        private CategoryManager _categoryManager;
        private SupplierManager _supplierManager;

        public AddProductModel(ProductManager productManager
            , CategoryManager categoryManager, SupplierManager supplierManager)
        {
            _productManager = productManager;
            _categoryManager = categoryManager;
            _supplierManager = supplierManager;
            product = new Product();
            supplier = (List<Supplier>)_supplierManager.GetSuppliers();
            category = (List<Category>)_categoryManager.GetCategorys();
        }

        public void OnGet()
        {
         
        }

        public IActionResult OnPostAddProduct()
        {
            string productName = Request.Form["product.ProductName"];
            string supplierId = Request.Form["product.SupplierId"];
            string categoryId = Request.Form["product.CategoryId"];
            string quantityPerUnit = Request.Form["product.QuantityPerUnit"];
            string unitPrice = Request.Form["product.UnitPrice"];
            string unitsInStock = Request.Form["product.UnitsInStock"];
            string unitsOnOrder = Request.Form["product.UnitsOnOrder"];
            string reorderLevel = Request.Form["product.ReorderLevel"];
            string discontinued = Request.Form["product.Discontinued"];

            if (string.IsNullOrEmpty(productName) || string.IsNullOrEmpty(supplierId) ||
                string.IsNullOrEmpty(categoryId) || string.IsNullOrEmpty(quantityPerUnit) ||
                string.IsNullOrEmpty(unitPrice) || string.IsNullOrEmpty(unitsInStock) ||
                string.IsNullOrEmpty(unitsOnOrder) || string.IsNullOrEmpty(reorderLevel) ||
                string.IsNullOrEmpty(discontinued))
            {
                ModelState.AddModelError("", "Please provide all the required fields.");
                return Page();
            }

            var productToAdd = new Product()
            {
                ProductName = productName,
                SupplierId = int.Parse(supplierId),
                CategoryId = int.Parse(categoryId),
                QuantityPerUnit = quantityPerUnit,
                UnitPrice = decimal.Parse(unitPrice),
                UnitsInStock = short.Parse(unitsInStock),
                UnitsOnOrder = short.Parse(unitsOnOrder),
                ReorderLevel = short.Parse(reorderLevel),
                Discontinued = bool.Parse(discontinued)
            };
            try
            {
                _productManager.AddProduct(productToAdd);
                TempData["SuccessMessage"] = "Product added successfully.";
            }catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return Page();
        }
    }
}
