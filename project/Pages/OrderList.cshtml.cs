using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using project.Business;
using project.Models;

namespace project.Pages
{
    public class OrderListModel : PageModel
    {

        public List<Order> Orders { get; set; }

        private OrderManager _orderManager;


        public int PageSize { get; set; } = 10;
        public int CurrentPage { get; set; } = 1;
        public int TotalPages => (int)Math.Ceiling(Orders.Count / (double)PageSize);

        public List<Order> DisplayOrders => Orders.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();




        public OrderListModel(OrderManager orderManager)
        {
            _orderManager = orderManager; 
            Orders = (List<Order>)orderManager.GetOrders();
        }

        public void OnGet()
        {
        }

        public IActionResult OnGetGoToPage(int pageIndex)
        {
            CurrentPage = pageIndex;



            return Page();
        }
    }
}
