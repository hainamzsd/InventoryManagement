using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class AddEmployeeModel : PageModel
    {
        [BindProperty]
        public Employee employee { get; set; }

        private EmployeeManager _employeeManager;

        public AddEmployeeModel(EmployeeManager employeeManager)
        {
            _employeeManager = employeeManager;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPostAddEmployee() {
            var employeeAdd = new Employee()
            {
                LastName = employee.LastName,
                FirstName = employee.FirstName,
                Title = employee.Title,
                TitleOfCourtesy = employee.TitleOfCourtesy,
                BirthDate = employee.BirthDate,
                HireDate = employee.HireDate,
                Address = employee.Address,
                City = employee.City,
                Region = employee.Region,
                PostalCode = employee.PostalCode,
                Country = employee.Country,
                HomePhone = employee.HomePhone,
                Extension = employee.Extension,
                Notes = employee.Notes,
            };

            try
            {
                _employeeManager.AddEmployee(employeeAdd);
                TempData["SuccessMessage"] = "Employee added successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            return Page();
        }
    }
}
