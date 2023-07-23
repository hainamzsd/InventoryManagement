using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
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

        public IActionResult OnGetExportToExcel()
        {
            var data = Suppliers;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Supplier Data");

                worksheet.Cells[1, 1].Value = "Company Name";
                worksheet.Cells[1, 2].Value = "Contact Name";
                worksheet.Cells[1, 3].Value = "Contact Title";
                worksheet.Cells[1, 4].Value = "Address";
                worksheet.Cells[1, 5].Value = "City";
                worksheet.Cells[1, 6].Value = "Region";
                worksheet.Cells[1, 7].Value = "Postal Code";
                worksheet.Cells[1, 8].Value = "Country";
                worksheet.Cells[1, 9].Value = "Phone";
                worksheet.Cells[1, 10].Value = "Fax";
                int row = 2;
                foreach (var supplier in data)
                {
                    worksheet.Cells[row, 1].Value = supplier.CompanyName;
                    worksheet.Cells[row, 2].Value = supplier.ContactName;
                    worksheet.Cells[row, 3].Value = supplier.ContactTitle;
                    worksheet.Cells[row, 4].Value = supplier.Address;
                    worksheet.Cells[row, 5].Value = supplier.City;
                    worksheet.Cells[row, 6].Value = supplier.Region;
                    worksheet.Cells[row, 7].Value = supplier.PostalCode;
                    worksheet.Cells[row, 8].Value = supplier.Country;
                    worksheet.Cells[row, 9].Value = supplier.Phone;
                    worksheet.Cells[row, 10].Value = supplier.Fax;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "SupplierData.xlsx");
            }
        }
    }
}
