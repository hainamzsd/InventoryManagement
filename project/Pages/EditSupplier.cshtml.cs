using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class EditSupplierModel : PageModel
    {
        [BindProperty]
        public Supplier supplier { get; set; }

        private SupplierManager _supplierManager;

        public EditSupplierModel(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
        }

        public IActionResult OnGet(int supplierId)
        {
            supplier = _supplierManager.GetSupplierById(supplierId);
            if(supplier == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostEditSupplier()
        {
            var existSupplier = _supplierManager.GetSupplierById(supplier.SupplierId);
            if(existSupplier == null)
            {
                return NotFound();
            }

            existSupplier.CompanyName = supplier.CompanyName ?? existSupplier.CompanyName;
            existSupplier.ContactName = supplier.ContactName ?? existSupplier.ContactName;
            existSupplier.ContactTitle = supplier.ContactTitle ?? existSupplier.ContactTitle;
            existSupplier.Address = supplier.Address ?? existSupplier.Address;
            existSupplier.City = supplier.City ?? existSupplier.City;
            existSupplier.Region = supplier.Region ?? existSupplier.Region;
            existSupplier.PostalCode = supplier.PostalCode ?? existSupplier.PostalCode;
            existSupplier.Country = supplier.Country ?? existSupplier.Country;
            existSupplier.Phone = supplier.Phone ?? existSupplier.Phone;
            existSupplier.Fax = supplier.Fax ?? existSupplier.Fax;
            try
            {
                _supplierManager.UpdateSupplier(existSupplier);
                TempData["SuccessMessage"] = "Supplier updated successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return Page();
        }
    }
}
