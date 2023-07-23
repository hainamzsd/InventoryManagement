using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class EditEmployeeModel : PageModel
    {
        [BindProperty]
        public Employee employee { get; set; }

        private EmployeeManager _employeeManager;


        public EditEmployeeModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }
        public IActionResult OnGet(int employeeId)
        {
            employee = _employeeManager.GetEmployeeById(employeeId);
            if(employee == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostEditEmployee()
        {
            var existingEmployee = _employeeManager.GetEmployeeById(employee.EmployeeId);
            if(existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.LastName = string.IsNullOrEmpty(employee.LastName) ? existingEmployee.LastName : employee.LastName;
            existingEmployee.FirstName = string.IsNullOrEmpty(employee.FirstName) ? existingEmployee.FirstName : employee.FirstName;
            existingEmployee.Title = string.IsNullOrEmpty(employee.Title) ? existingEmployee.Title : employee.Title;
            existingEmployee.TitleOfCourtesy = string.IsNullOrEmpty(employee.TitleOfCourtesy) ? existingEmployee.TitleOfCourtesy : employee.TitleOfCourtesy;
            existingEmployee.BirthDate = employee.BirthDate ?? existingEmployee.BirthDate;
            existingEmployee.HireDate = employee.HireDate ?? existingEmployee.HireDate;
            existingEmployee.Address = employee.Address ?? existingEmployee.Address;
            existingEmployee.City = employee.City ?? existingEmployee.City;
            existingEmployee.Region = employee.Region ?? existingEmployee.Region;
            existingEmployee.PostalCode = employee.PostalCode ?? existingEmployee.PostalCode;
            existingEmployee.Country = employee.Country ?? existingEmployee.Country;
            existingEmployee.HomePhone = employee.HomePhone ?? existingEmployee.HomePhone;
            existingEmployee.Extension = employee.Extension ?? existingEmployee.Extension;
            existingEmployee.Notes = employee.Notes ?? existingEmployee.Notes;
            try
            {
                _employeeManager.UpdateEmployee(existingEmployee);
                TempData["SuccessMessage"] = "Employee updated successfully.";
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
       
            return Page();

        }
    }
}
