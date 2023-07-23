using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml.Style;
using OfficeOpenXml;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class EmloyeeModel : PageModel
    {
        public List<Employee>  Employees { get; set; }

        private EmployeeManager _employeeManager;

        public List<Employee> searchList { get; set; }
        public int PageSize { get; set; } = 3;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling(Employees.Count / (double)PageSize);
        public List<Employee> DisplayEmployees => Employees.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();

        public EmloyeeModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
            Employees = (List<Employee>)_employeeManager.GetEmployees();
        }
        public void OnGet()
        {
        }

        public IActionResult OnGetDeleteEmployee(int employeeId)
        {
            var employee = _employeeManager.GetEmployeeById(employeeId);
            if (employee == null)
            {
                return NotFound();
            }
            try
            {
                _employeeManager.DeleteEmployee(employeeId);
                TempData["Success Message"] = "Delete successfull";
            }
            catch (Exception ex) {
                TempData["Fail Message"] = "Delete Fail due to " + ex.Message;
            }
      
            return Page();
        }

        public IActionResult OnGetGoToPage(int pageIndex)
        {
            CurrentPage = pageIndex;



            return Page();
        }

        public IActionResult OnPostSearch(string firstName)
        {
            searchList = _employeeManager.GetEmployeesByName(firstName);
            return Page();
        }

        public IActionResult OnGetExportToExcel()
        {
            var data = Employees;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Employee Data");

                worksheet.Cells[1, 1].Value = "Last Name";
                worksheet.Cells[1, 2].Value = "First Name";
                worksheet.Cells[1, 3].Value = "Title";
                worksheet.Cells[1, 4].Value = "Title of courtesy";
                worksheet.Cells[1, 5].Value = "Birth Date";
                worksheet.Cells[1, 6].Value = "Hire Date";
                worksheet.Cells[1, 7].Value = "Address";
                worksheet.Cells[1, 8].Value = "City";
                worksheet.Cells[1, 9].Value = "Region";
                worksheet.Cells[1, 10].Value = "Postal Code";
                worksheet.Cells[1, 11].Value = "Home Phone";
                worksheet.Cells[1, 12].Value = "Extension";
                worksheet.Cells[1, 13].Value = "Notes";

                int row = 2;
                foreach (var employee in data)
                {
                    worksheet.Cells[row, 1].Value = employee.LastName;
                    worksheet.Cells[row, 2].Value = employee.FirstName;
                    worksheet.Cells[row, 3].Value = employee.Title;
                    worksheet.Cells[row, 4].Value = employee.TitleOfCourtesy;
                    worksheet.Cells[row, 5].Value = employee.BirthDate;
                    worksheet.Cells[row, 6].Value = employee.HireDate;
                    worksheet.Cells[row, 7].Value = employee.Address;
                    worksheet.Cells[row, 8].Value = employee.City;
                    worksheet.Cells[row, 9].Value = employee.Region;
                    worksheet.Cells[row, 10].Value = employee.PostalCode;
                    worksheet.Cells[row, 11].Value = employee.HomePhone;
                    worksheet.Cells[row, 12].Value = employee.Extension;
                    worksheet.Cells[row, 13].Value = employee.Notes;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "EmployeeData.xlsx");
            }
        }
    }
}
