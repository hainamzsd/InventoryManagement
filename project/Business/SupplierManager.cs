using project.Models;
using project.Repository;

namespace project.Business
{
    public class SupplierManager
    {
        private readonly IRepository<Supplier> _supplierRepository;

        public SupplierManager(IRepository<Supplier> supplierRepository)
        {
            _supplierRepository = supplierRepository;
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
            _supplierRepository.Delete(supplierId);
        }
    }
}
