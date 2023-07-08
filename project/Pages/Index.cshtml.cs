using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;
using project.SessionExtensions;

namespace project.Pages
{

    public class IndexModel : PageModel
    {
        private NorthwindContext _context = new NorthwindContext();
        public List<Product> Products { get; set; }

        private ProductManager _productManager;


        public IndexModel(ProductManager productManager)
        {
            _productManager = productManager;
            Products = (List<Product>)_productManager.GetProducts();
        }

        public IActionResult OnGet()
        {
            if (HttpContext.Session.GetString("Username") != "admin")
            {
                return RedirectToPage("/Login");
            }
            Products = (List<Product>)_productManager.GetProducts();
            // Continue with the page logic
            return Page();
        }

        public IActionResult OnPostDeleteProduct(int productId)
        {
            var product = _productManager.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            _productManager.DeleteProduct(productId);

            // Redirect to a success page or another appropriate location
            return Page();
        }
    }
}