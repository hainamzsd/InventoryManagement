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

        public int CurrentPage { get; set; }
        public int TotalProducts { get; set; }
        public int ProductsPerPage { get; set; }


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
            CurrentPage = 1;
            ProductsPerPage = 10; 

            Products = (List<Product>)_productManager.GetProducts();

            TotalProducts = Products.Count;

            Products = Products.Skip((CurrentPage - 1) * ProductsPerPage)
                               .Take(ProductsPerPage)
                               .ToList();

            return Page();
        }

        public IActionResult OnGetDeleteProduct(int productId)
        {
            var product = _productManager.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }

            _productManager.DeleteProduct(productId);

            // Redirect to a success page or another appropriate location
            return RedirectToPage();
        }

        public IActionResult OnGetPage(int page)
        {
            CurrentPage = page;

            Products = (List<Product>)_productManager.GetProducts();

            TotalProducts = Products.Count;

            Products = Products.Skip((CurrentPage - 1) * ProductsPerPage)
                               .Take(ProductsPerPage)
                               .ToList();

            return Page();
        }
    }
}