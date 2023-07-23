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
    }
}
