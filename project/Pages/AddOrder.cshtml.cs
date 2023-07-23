using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class AddOrderModel : PageModel
    {
        [BindProperty]
        public Order order { get; set; }
        private OrderManager _orderManager;

        
        private EmployeeManager _employeeManager;
        private CustomerManager _customerManager;
        private ProductManager _productManager;
        public List<Employee> Employees { get; set; }
        
        public List<Product> Products { get; set; } 
        public List<Customer> Customers { get; set; }

        public AddOrderModel(OrderManager orderManager, EmployeeManager employeeManager,
            CustomerManager customerManager, ProductManager productManager)
        {
            _orderManager = orderManager; 
            _employeeManager = employeeManager;
            _customerManager = customerManager;
            _productManager = productManager;
            order = new Order();

            Employees = (List<Employee>)employeeManager.GetEmployees();
            Customers = (List<Customer>)customerManager.GetCustomers();
            Products = (List<Product>)productManager.GetProducts();
        }
        public void OnGet()
        {
        }

        public IActionResult OnPostAddOrder()
        {
            var orderAdd = new Order()
            {
                CustomerId = order.CustomerId,
                EmployeeId = order.EmployeeId,
                OrderDate = order.OrderDate,
                RequiredDate = order.RequiredDate,
                ShippedDate = order.ShippedDate,
                ShipVia = order.ShipVia,
                Freight = order.Freight,
                ShipName = order.ShipName,
                ShipAddress = order.ShipAddress,
                ShipCity = order.ShipCity,
                ShipRegion = order.ShipRegion,
                ShipPostalCode = order.ShipPostalCode,
                ShipCountry = order.ShipCountry,
            };

            try
            {
                _orderManager.AddOrder(orderAdd);
                TempData["SuccessMessage"] = "Order added successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                TempData["Fail"] = "Order added fail.";
            }
            return Page();
        }
    }
}
