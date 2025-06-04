using IncidentManagement.BusinessLogic.Category;
using IncidentManagement.BusinessLogic.Roles;
using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.UIModels.UICategory;
using IncidentManagement.WebAPI.IMAttribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryController(ICategoryRepository repository) 
        {
            _categoryRepository = repository;
        }


        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAllItems();
            return Ok(new { result = categories });
        }

        [HttpGet("GetCategory/{id}")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Get(string id)
        {
            var categoryId = Guid.Empty;

            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out categoryId))
            {
                return BadRequest("Invalid category ID.");
            }


            var category = await _categoryRepository.GetItemById<Guid>(categoryId);
            return Ok(new { result = category });
        }


        [HttpPost("AddCategory")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Post([FromBody] UICategoryModel model)
        {
            if(ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var categoryModel = new CategoryModel()
            {
                CategoryId = Guid.NewGuid(),
                CategoryName = model.CategoryName,
                CategoryDescription = model.CategoryDescription.Trim(),
                CreatedBy = baseUserModel.UserId,
            };

            var categories = await _categoryRepository.AddItem(categoryModel);
            return Ok(new { result = categories });
        }


        [HttpPut("ModifyCategory/{id}")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Put(Guid id,[FromBody] UICategoryModel model)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }

            var categoryModel = new CategoryModel()
            {
                CategoryId = id,
                CategoryName = model.CategoryName,
                CategoryDescription = model.CategoryDescription.Trim()
            };

            var result = await _categoryRepository.EditItem(categoryModel);
            return Ok(new { result = result });
        }

        [HttpDelete("DeleteCategory/{id}")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Delete(string id)
        {
            var deleteId = Guid.Parse(id);
            
            if(deleteId == Guid.Empty)
            {
                return BadRequest("Invalid category ID.");
            }

            var result = await _categoryRepository.DeleteItem(deleteId);
            return Ok(new { result = result });
        }


    }
}
