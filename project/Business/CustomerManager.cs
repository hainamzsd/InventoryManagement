using project.Models;
using project.Repository;

namespace project.Business
{
    public class CustomerManager
    {

        private readonly IRepository<Customer> _customerRepository;

        private NorthwindContext _northwindContext;

        public CustomerManager(IRepository<Customer> customerRepository, NorthwindContext northwindContext)
        {
            _customerRepository = customerRepository;
            _northwindContext = northwindContext;
        }

        public IEnumerable<Customer> GetCustomers()
        {
            return _customerRepository.GetAll();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _customerRepository.GetById(customerId);
        }

        public void AddCustomer(Customer customer)
        {
            _customerRepository.Insert(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            _customerRepository.Update(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            _customerRepository.Delete(customerId);
        }

    }
}
