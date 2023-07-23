using Microsoft.IdentityModel.Tokens;
using project.Models;
using project.Repository;

namespace project.Business
{
    public class SupplierManager
    {
        private readonly IRepository<Supplier> _supplierRepository;
        private NorthwindContext _northwindContext;
        public SupplierManager(IRepository<Supplier> supplierRepository, NorthwindContext northwindContext)
        {
            _supplierRepository = supplierRepository;
            _northwindContext = northwindContext;
        }

        public IEnumerable<Supplier> GetSuppliers()
        {
            return _supplierRepository.GetAll();
        }

        public Supplier GetSupplierById(int supplierId)
        {
            return _supplierRepository.GetById(supplierId);
        }

        public void AddSupplier(Supplier supplier)
        {
            _supplierRepository.Insert(supplier);
        }

        public void UpdateSupplier(Supplier supplier)
        {
            _supplierRepository.Update(supplier);
        }

        public void DeleteSupplier(int supplierId)
        {
            var products = _northwindContext.Products.Where(p => p.SupplierId == supplierId).ToList();
            if(products.Count > 0)
            {
                foreach(var product in products)
                {
                    var orderDetails = _northwindContext.OrderDetails.Where(o => o.ProductId == product.ProductId).ToList();
                    if(orderDetails.Count > 0)
                    {
                        _northwindContext.OrderDetails.RemoveRange(orderDetails);
                    }
                }
                _northwindContext.Products.RemoveRange(products);
            }
            _supplierRepository.Delete(supplierId);
        }

        public List<Supplier> GetSupplierByName(string name)
        {
            if (!name.IsNullOrEmpty())
            {
                return _northwindContext.Suppliers.Where(x => x.CompanyName.Contains(name)).ToList();
            }
            return null;
        }
    }
}
