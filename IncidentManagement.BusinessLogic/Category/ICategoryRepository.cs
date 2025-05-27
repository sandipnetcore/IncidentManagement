
using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.UIModels.UICategory;

namespace IncidentManagement.BusinessLogic.Category
{
    public interface ICategoryRepository
    {
        Task<List<UICategoryModel>> GetAllCategories();
        Task<bool> AddCategory(CategoryModel model);
        Task<bool> EditCategory(CategoryModel model);
        Task<bool> DeleteCategory(Guid id);

    }
}
