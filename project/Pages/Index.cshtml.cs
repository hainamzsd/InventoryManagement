using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
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

        public List<Product> searchList { get; set; }

        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling(Products.Count / (double)PageSize);
        public List<Product> DisplayProducts => Products.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();




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
          
            return Page();
        }

        public IActionResult OnGetDeleteProduct(int productId)
        {
            var product = _productManager.GetProductById(productId);
            if (product == null)
            {
                return NotFound();
            }
            try
            {
                _productManager.DeleteProduct(productId);
                TempData["Success Message"] = "Delete successfull";
            }
            catch (Exception ex)
            {
                TempData["Fail Message"] = "Delete Fail due to " + ex.Message;
            }

            // Redirect to a success page or another appropriate location
            return RedirectToPage();
        }

        public IActionResult OnGetGoToPage(int pageIndex)
        {
            CurrentPage = pageIndex;

        

            return Page();
        }

        public IActionResult OnGetExportToExcel()
        {
            // Get the data to export (in this case, it's the Model.DisplayProducts)
            var data = Products;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Product Data");

                worksheet.Cells[1, 1].Value = "Product ID";
                worksheet.Cells[1, 2].Value = "Name";
                worksheet.Cells[1, 3].Value = "Unit in stock";
                worksheet.Cells[1, 4].Value = "Unit on orders";
                worksheet.Cells[1, 5].Value = "Price";

                int row = 2;
                foreach (var product in data)
                {
                    worksheet.Cells[row, 1].Value = product.ProductId;
                    worksheet.Cells[row, 2].Value = product.ProductName;
                    worksheet.Cells[row, 3].Value = product.UnitsInStock;
                    worksheet.Cells[row, 4].Value = product.UnitsOnOrder;
                    worksheet.Cells[row, 5].Value = product.UnitPrice;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "ProductData.xlsx");
            }
        }

        public IActionResult OnPostSearch(string productName)
        {
            searchList = _productManager.GetProductsByName(productName);
            return Page();
        }

    }
}