using project.Models;
using project.Repository;

namespace project.Business
{
    public class OrderManager
    {
        private readonly IRepository<Order> _orderRepository;

        private NorthwindContext _northwindContext;

        public OrderManager(IRepository<Order> orderRepository, NorthwindContext northwindContext)
        {
            _orderRepository = orderRepository;
            _northwindContext = northwindContext;
        }

        public IEnumerable<Order> GetOrders()
        {
            return _orderRepository.GetAll();
        }

        public Order GetOrderById(int orderId)
        {
            return _orderRepository.GetById(orderId);
        }

        public void AddOrder(Order order)
        {
            _orderRepository.Insert(order);
        }

        public void UpdateOrder(Order order)
        {
            _orderRepository.Update(order);
        }

        public void DeleteOrder(int orderId)
        {
            var orderDetails = _northwindContext.OrderDetails.Where(x => x.OrderId == orderId).ToList();
            _northwindContext.OrderDetails.RemoveRange(orderDetails);
            _orderRepository.Delete(orderId);
        }

      
    }
}
