using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.UIModels.UICategory;
using IncidentManagement.EntityFrameWork.DBOperations;

namespace IncidentManagement.BusinessLogic.Category
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext _dataContext;
        
        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<UICategoryModel>> GetAllCategories()
        {
            return await Task.FromResult(
                _dataContext.Categories
                    .Where(c => c.IsActive)
                    .Select(c => new UICategoryModel
                    {
                        CategoryId = c.CategoryId,
                        CategoryName = c.CategoryName,
                    }).ToList()
            );
        }

        public async Task<bool> AddCategory(CategoryModel model)
        {
            bool isAdded = false;

            try
            {
                _dataContext.Categories.Add(model);
                _dataContext.SaveChanges();
                isAdded = true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding category: {ex.Message}");
            }

            return await Task.FromResult<bool>(isAdded);
        }

        public async Task<bool> EditCategory(CategoryModel model)
        {
            bool isUpdated = false;

            try
            {
                _dataContext.Update<CategoryModel>(model);
                _dataContext.SaveChanges();
                isUpdated = true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding category: {ex.Message}");
            }

            return await Task.FromResult<bool>(isUpdated);
        }

        public async Task<bool> DeleteCategory(Guid modelcategoryId)
        {
            bool isDeleted = false;

            try
            {
                var categoryModel = _dataContext.Categories.FirstOrDefault(c => c.CategoryId == modelcategoryId);
                if (categoryModel == null)
                {
                    return await Task.FromResult<bool>(false);
                }

                _dataContext.Remove<CategoryModel>(categoryModel);
                _dataContext.SaveChanges();
                isDeleted = true;
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine($"Error adding category: {ex.Message}");
            }

            return await Task.FromResult<bool>(isDeleted);
        }
    }
}
