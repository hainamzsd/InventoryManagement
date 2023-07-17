using project.Models;
using project.Repository;

namespace project.Business
{
    public class ProductManager
    {
        NorthwindContext _context;

        private readonly IRepository<Product> _productRepository ;

        public ProductManager(IRepository<Product> productRepository, NorthwindContext context)
        {
            _productRepository = productRepository;
            _context = context;
        }

        public IEnumerable<Product> GetProducts()
        {
            return _productRepository.GetAll();
        }

        public Product GetProductById(int productId)
        {
            return _productRepository.GetById(productId);
        }

        public void AddProduct(Product product)
        {
            _productRepository.Insert(product);
        }

        public void UpdateProduct(Product product)
        {
            _productRepository.Update(product);
        }

        public void DeleteProduct(int productId)
        {
            var orderDetails = _context.OrderDetails.Where(x => x.ProductId == productId).ToList();
            _context.RemoveRange(orderDetails);
            _productRepository.Delete(productId);
        }
    }
}
