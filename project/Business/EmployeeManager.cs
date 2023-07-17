using project.Models;
using project.Repository;

namespace project.Business
{
    public class EmployeeManager
    {

        private readonly IRepository<Employee> _employeeRepository;

        private NorthwindContext _northwindContext;

        public EmployeeManager(IRepository<Employee> employeeRepository, NorthwindContext northwindContext)
        {
            _employeeRepository = employeeRepository;
            _northwindContext = northwindContext;
        }

        public IEnumerable<Employee> GetEmployees()
        {
            return _employeeRepository.GetAll();
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return _employeeRepository.GetById(employeeId);
        }

        public void AddEmployee(Employee employee)
        {
            _employeeRepository.Insert(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            _employeeRepository.Update(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            _employeeRepository.Delete(employeeId);
        }


    }
}
