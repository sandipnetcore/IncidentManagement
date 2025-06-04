using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.UIModels.UICategory;
using IncidentManagement.EntityFrameWork.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.BusinessLogic.Category
{
    public class CategoryRepository: ICategoryRepository
    {
        private readonly DataContext _dataContext;
        
        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<List<UICategoryModel>> GetAllItems()
        {
            var result = _dataContext.Categories.FirstOrDefault();

            return await Task.FromResult(
                _dataContext.Categories
                    .Where(c => c.IsActive)
                    .Select(c => new UICategoryModel
                    {
                        CategoryId = c.CategoryId.ToString(),
                        CategoryName = c.CategoryName,
                        CategoryDescription = c.CategoryDescription,
                        CreatedBy = c.User.UserName,
                        CreatedOn = c.CreatedOn.ToString("dd/MMM/yyyy"),
                        ModifiedBy = c.ModifiedBy == Guid.Empty ? (_dataContext.Users.FirstOrDefault(u=> u.UserId == c.ModifiedBy)).UserName: "-",
                        ModifiedOn = !c.ModifiedOn.HasValue ? "-" : c.ModifiedOn.Value.ToString("dd/MMM/yyyy"),
                    }).ToList()
            );
        }

        public async Task<bool> AddItem(CategoryModel model)
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

        public async Task<bool> EditItem(CategoryModel model)
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

        public async Task<bool> DeleteItem<T>(T id)
        {
            bool isDeleted = false;
            var modelcategoryId = Guid.Parse(id.ToString());
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
        
        public async Task<UICategoryModel?> GetItemById<T>(T id)
        {
            var guidId = Guid.Parse(id.ToString());

            return await _dataContext.Categories.Where(c=> c.IsActive && c.CategoryId == guidId).Select( ui=> new UICategoryModel
            {
                CategoryId = guidId.ToString(),
                CategoryName = ui.CategoryName,
                CategoryDescription = ui.CategoryDescription,
                CreatedBy = ui.User.UserName,
                ModifiedBy = ui.ModifiedBy == Guid.Empty ? 
                                "-" : 
                                _dataContext.Users.FirstOrDefault(u=> u.UserId == ui.ModifiedBy).UserName,
                CreatedOn = ui.CreatedOn.ToString("dd/MMM/yyyy"),
                ModifiedOn = ui.ModifiedOn.HasValue? ui.ModifiedOn.Value.ToString("dd/MMM/yyyy") : "-",
            }).FirstOrDefaultAsync();
        }
    }
}
