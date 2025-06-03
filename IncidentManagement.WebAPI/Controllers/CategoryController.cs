using IncidentManagement.BusinessLogic.Category;
using IncidentManagement.BusinessLogic.Roles;
using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.UIModels.UICategory;
using IncidentManagement.WebAPI.IMAttribute;
using Microsoft.AspNetCore.Http;
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
        public async Task<IActionResult> Get(string id)
        {
            var categoryId = Guid.Empty;

            if (string.IsNullOrEmpty(id) || !Guid.TryParse(id, out categoryId))
            {
                return BadRequest("Invalid category ID.");
            }


            var categories = await _categoryRepository.GetItemById<Guid>(categoryId);
            return Ok(new { result = categories });
        }


        [HttpPost("AddCategory")]
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
                CategoryDescription = model.CategoryDescription.Trim()
            };

            var categories = await _categoryRepository.AddItem(categoryModel);
            return Ok(new { result = categories });
        }


        [HttpPost("ModifyCategory/{id}")]
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

            var categories = await _categoryRepository.EditItem(categoryModel);
            return Ok(new { result = categories });
        }

        [HttpPost("DeleteCategory/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var deleteId = Guid.Empty;
            if (Guid.TryParse(id, out deleteId))
            {
                return BadRequest("Invalid category ID.");
            }


            var categories = await _categoryRepository.DeleteItem(deleteId);
            return Ok(new { result = categories });
        }


    }
}
