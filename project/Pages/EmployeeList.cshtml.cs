using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
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

    }
}
