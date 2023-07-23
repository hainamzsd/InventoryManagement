using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class SupplierListModel : PageModel
    {
        public List<Supplier> Suppliers { get; set; }

        public List<Supplier> searchList { get; set; }

        private SupplierManager _supplierManager;

        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling(Suppliers.Count / (double)PageSize);
        public List<Supplier> DisplaySuppliers => Suppliers.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
        public SupplierListModel(SupplierManager supplierManager)
        {
            _supplierManager = supplierManager;
            Suppliers = (List<Supplier>)_supplierManager.GetSuppliers(); 
        }
        public void OnGet()
        {
        }

        public IActionResult OnGetDeleteSupplier(int supplierId)
        {
            var supplier = _supplierManager.GetSupplierById(supplierId);
            if (supplier == null)
            {
                return NotFound();
            }
            try
            {
                _supplierManager.DeleteSupplier(supplierId);
                TempData["Success Message"] = "Delete successfull";
            }catch(Exception ex)
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

        public IActionResult OnPostSearch(string companyName)
        {
            searchList = _supplierManager.GetSupplierByName(companyName);
            return Page();
        }
    }
}
