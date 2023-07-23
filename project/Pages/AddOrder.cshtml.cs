using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class AddOrderModel : PageModel
    {
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
            string customerId = Request.Form["order.CustomerId"];
            string employeeId = Request.Form["order.EmployeeId"];
            string orderDate = Request.Form["order.OrderDate"];
            string requireDate = Request.Form["order.RequiredDate"];
            string shipDate = Request.Form["order.ShippedDate"];
            string shipVia = Request.Form["order.ShipVia"];
            string freight = Request.Form["order.Freight"];
            string shipName = Request.Form["order.ShipName"];
            string shipAddress = Request.Form["order.ShipAddress"];
            string shipCity = Request.Form["order.ShipCity"];
            string shipRegion = Request.Form["order.ShipRegion"];
            string shipPostalCode = Request.Form["order.ShipPostalCode"];
            string shipCountry = Request.Form["order.ShipCountry"];

            if (string.IsNullOrEmpty(customerId) || string.IsNullOrEmpty(employeeId) ||
    string.IsNullOrEmpty(orderDate) || string.IsNullOrEmpty(requireDate) ||
    string.IsNullOrEmpty(shipDate) || string.IsNullOrEmpty(shipVia) ||
    string.IsNullOrEmpty(freight) || string.IsNullOrEmpty(shipName) ||
    string.IsNullOrEmpty(shipAddress) || string.IsNullOrEmpty(shipCity) ||
    string.IsNullOrEmpty(shipRegion) || string.IsNullOrEmpty(shipPostalCode) ||
    string.IsNullOrEmpty(shipCountry))
            {
                ModelState.AddModelError("", "Please provide all the required fields.");
                return Page();
            }

            var orderToAdd = new Order()
            {
                CustomerId = customerId,
                EmployeeId = int.Parse(employeeId),
                OrderDate = DateTime.Parse(orderDate),
                RequiredDate = DateTime.Parse(requireDate),
                ShippedDate = DateTime.Parse(shipDate),
                ShipVia = int.Parse(shipVia),
                Freight = decimal.Parse(freight),
                ShipName = shipName,
                ShipAddress = shipAddress,
                ShipCity = shipCity,
                ShipRegion = shipRegion,
                ShipPostalCode = shipPostalCode,
                ShipCountry = shipCountry
            };


            return Page();
        }
    }
}
