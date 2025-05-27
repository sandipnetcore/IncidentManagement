using IncidentManagement.BusinessLogic.Category;
using IncidentManagement.BusinessLogic.Roles;
using IncidentManagement.WebAPI.IMAttribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepository _categoryRepository;
        
        public CategoryController(ICategoryRepository repository) 
        {
            _categoryRepository = repository;
        }


        [HttpGet("GetAllCategories")]
        public async Task<IActionResult> Get()
        {
            var categories = await _categoryRepository.GetAllCategories();
            return Ok(new { result = categories });
        }
    }
}
