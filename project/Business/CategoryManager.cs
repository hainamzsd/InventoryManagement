using project.Models;
using project.Repository;

namespace project.Business
{
    public class CategoryManager
    {
        private readonly IRepository<Category> _categoryRepository;

        public CategoryManager(IRepository<Category> categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public IEnumerable<Category> GetCategorys()
        {
            return _categoryRepository.GetAll();
        }

        public Category GetCategoryById(int categoryId)
        {
            return _categoryRepository.GetById(categoryId);
        }

        public void AddCategory(Category category)
        {
            _categoryRepository.Insert(category);
        }

        public void UpdateCategory(Category category)
        {
            _categoryRepository.Update(category);
        }

        public void DeleteCategory(int categoryId)
        {
            _categoryRepository.Delete(categoryId);
        }
    }
}
