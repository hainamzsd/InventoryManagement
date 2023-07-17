using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class EditOrderModel : PageModel
    {
        private OrderManager _orderManager;
        private EmployeeManager _employeeManager;
        private CustomerManager _customerManager;

        public List<Employee> Employees { get; set; }
        public List<Customer> Customers { get; set; }
        [BindProperty]
        public Order order { get; set; }

        public EditOrderModel(OrderManager orderManager, EmployeeManager employeeManager,
            CustomerManager customerManager)
        {
            _orderManager = orderManager;
            _employeeManager = employeeManager;
            _customerManager = customerManager;

            Employees = (List<Employee>) employeeManager.GetEmployees();
            Customers = (List<Customer>) customerManager.GetCustomers();
            
        }

        public IActionResult OnGet(int orderId)
        {
            order = _orderManager.GetOrderById(orderId);
            if (order == null)
            {
                return NotFound();
            }
            return Page();
        }

        public IActionResult OnPostEditOrder()
        {
            var existingOrder = _orderManager.GetOrderById(order.OrderId);
            if (existingOrder == null)
            {
                return NotFound();
            }
            existingOrder.CustomerId = order.CustomerId ?? existingOrder.CustomerId;
            existingOrder.EmployeeId = order.EmployeeId ?? existingOrder.EmployeeId;
            existingOrder.OrderDate = order.OrderDate ?? existingOrder.OrderDate;
            existingOrder.RequiredDate = order.RequiredDate ?? existingOrder.RequiredDate;
            existingOrder.ShippedDate = order.ShippedDate ?? existingOrder.ShippedDate;
            existingOrder.ShipVia = order.ShipVia ?? existingOrder.ShipVia;
            existingOrder.Freight = order.Freight ?? existingOrder.Freight;
            existingOrder.ShipName = order.ShipAddress ?? existingOrder.ShipAddress;
            existingOrder.ShipAddress = order.ShipAddress ?? existingOrder.ShipAddress;
            existingOrder.ShipRegion = order.ShipRegion ?? existingOrder.ShipRegion;
            existingOrder.ShipPostalCode = order.ShipPostalCode ?? existingOrder.ShipPostalCode;
            existingOrder.ShipCountry = order.ShipCountry ?? existingOrder.ShipCountry;
            try
            {
                _orderManager.UpdateOrder(existingOrder);
                TempData["SuccessMessage"] = "Order edited successfully.";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                ModelState.AddModelError("", "Error edit " + ex.Message);
            }
            return Page();

        }
    }
}
