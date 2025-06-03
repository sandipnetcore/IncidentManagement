using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.UIModels.UICategory;

namespace IncidentManagement.BusinessLogic.Category
{
    public interface ICategoryRepository: ICrudOperations<CategoryModel, UICategoryModel>
    {

    }
}
