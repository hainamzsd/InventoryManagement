using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class AddSupplierModel : PageModel
    {
        [BindProperty]
        public Supplier supplier { get; set; }

        private SupplierManager _supplierManager;

        public AddSupplierModel(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostAddSupplier()
        {
            var supplierAdd = new Supplier() {
                CompanyName = supplier.CompanyName,
                ContactName = supplier.ContactName,
                ContactTitle = supplier.ContactTitle,
                Address = supplier.Address,
                City = supplier.City,
                Region = supplier.Region,
                PostalCode = supplier.PostalCode,   
                Country = supplier.Country,
                Phone = supplier.Phone,
                Fax = supplier.Fax,
            };
            try
            {
                _supplierManager.AddSupplier(supplierAdd);
                TempData["SuccessMessage"] = "Supplier added successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["Fail"] = "Supplier added fail.";
            }

            return Page();
        }
    }
}
