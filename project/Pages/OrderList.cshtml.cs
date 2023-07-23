using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
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

        public IActionResult OnGetDeleteOrder(int orderId)
        {
            var order = _orderManager.GetOrderById(orderId);
            if(order == null)
            {
                return NotFound();
            }
            _orderManager.DeleteOrder(orderId);
            TempData["Success Message"] = "Delete successfull"; 
            return Page();
        }

        public IActionResult OnGetGoToPage(int pageIndex)
        {
            CurrentPage = pageIndex;



            return Page();
        }

        public IActionResult OnGetExportToExcel()
        {
            var data = Orders;

            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Order Data");

                worksheet.Cells[1, 1].Value = "Customer ID";
                worksheet.Cells[1, 2].Value = "Employee Id";
                worksheet.Cells[1, 3].Value = "Order Date";
                worksheet.Cells[1, 4].Value = "Required Date";
                worksheet.Cells[1, 5].Value = "Shipped Date";
                worksheet.Cells[1, 6].Value = "Ship Via";
                worksheet.Cells[1, 7].Value = "Freight";
                worksheet.Cells[1, 8].Value = "Ship Name";
                worksheet.Cells[1, 9].Value = "Ship Address";
                worksheet.Cells[1, 10].Value = "Ship City";
                worksheet.Cells[1, 11].Value = "Ship Region";
                worksheet.Cells[1, 12].Value = "Ship Postal Code";
                worksheet.Cells[1, 13].Value = "Ship Country";

                int row = 2;
                foreach (var order in data)
                {
                    worksheet.Cells[row, 1].Value = order.CustomerId;
                    worksheet.Cells[row, 2].Value = order.EmployeeId;
                    worksheet.Cells[row, 3].Value = order.OrderDate;
                    worksheet.Cells[row, 4].Value = order.RequiredDate;
                    worksheet.Cells[row, 5].Value = order.ShippedDate;
                    worksheet.Cells[row, 6].Value = order.ShipVia;
                    worksheet.Cells[row, 7].Value = order.Freight;
                    worksheet.Cells[row, 8].Value = order.ShipName;
                    worksheet.Cells[row, 9].Value = order.ShipAddress;
                    worksheet.Cells[row, 10].Value = order.ShipCity;
                    worksheet.Cells[row, 11].Value = order.ShipRegion;
                    worksheet.Cells[row, 12].Value = order.ShipPostalCode;
                    worksheet.Cells[row, 13].Value = order.ShipCountry;
                    row++;
                }

                worksheet.Cells.AutoFitColumns();

                var stream = new MemoryStream(package.GetAsByteArray());
                return File(stream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "OrderData.xlsx");
            }
        }
    }
}
