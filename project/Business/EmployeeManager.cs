using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
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
            var orders = _northwindContext.Orders.Where(o => o.EmployeeId == employeeId).ToList();

            if (orders != null)
            {
                foreach (var order in orders)
                {
                    var orderDetails = _northwindContext.OrderDetails.Where(od => od.OrderId == order.OrderId).ToList();
                    if (orderDetails != null)
                    {
                        _northwindContext.OrderDetails.RemoveRange(orderDetails);
                    }
                }

                _northwindContext.Orders.RemoveRange(orders);
            }
            _employeeRepository.Delete(employeeId);
        }

        public List<Employee> GetEmployeesByName(string name)
        {
            if (!name.IsNullOrEmpty())
            {
                return _northwindContext.Employees.Where(x => x.FirstName.Contains(name)).ToList();
            }
            return null;
        }

    }
}
