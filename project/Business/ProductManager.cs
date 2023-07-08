using project.Models;
using project.Repository;

namespace project.Business
{
    public class ProductManager
    {

        private readonly IRepository<Product> _productRepository ;

        public ProductManager(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
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
            _productRepository.Delete(productId);
        }
    }
}
