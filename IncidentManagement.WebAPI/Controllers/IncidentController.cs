using IncidentManagement.BusinessLogic;
using IncidentManagement.BusinessLogic.Incident;
using IncidentManagement.BusinessLogic.Roles;
using IncidentManagement.DataModel.Incident;
using IncidentManagement.DataModel.UIModels.UIIncident;
using IncidentManagement.WebAPI.IMAttribute;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagement.WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class IncidentController : BaseController
    {
        private readonly IIncidentRepository _incidentRepository;

        public IncidentController(IIncidentRepository incidentRepository)
        {
            _incidentRepository = incidentRepository;
        }

        
        [HttpGet("GetAllIncident")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Get()
        {
            var allIncidents = await _incidentRepository.GetAllIncidents();
            return Ok(new { result = allIncidents});
        }

        [HttpGet("GetAllStatusTypes")]
        public async Task<IActionResult> GetAllStatus()
        {
            var allStatus = await _incidentRepository.GetAllStatusTypes();
            return Ok(new { result = allStatus });
        }

        [HttpGet("GetDetailsById/{id}")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Get(Guid id)
        {
            var incident = await _incidentRepository.GetIncidentCompleteDetailsById(id);

            return Ok(new { result = incident });
        }


        [HttpPost("CreateIncident")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Post([FromBody] UIIncidentModel model)
        {
            var isCreated = await _incidentRepository.CreateIncident(model, baseUserModel);

            return Ok(new { result = isCreated });
        }


        [HttpPost("AddComments/{Id}")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Post([FromBody] UICommentModel model)
        {
            var isUpdated = await _incidentRepository
                            .AddUserAndCommentsToIncident(model);

            return Ok(new { result = isUpdated });
        }


        [HttpPut("ChangeStatus/{id}")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole, IncidentManagementRoles.User)]
        public async Task<IActionResult> Put(Guid id, [FromBody] UIIncidentStatusModel changeStatusModel)
        {
            var isChanged = await _incidentRepository.ChangeIncidentStatus(id, changeStatusModel.StatusId); 
            
            return Ok(new { result = isChanged });
        }

     

        [HttpDelete("Delete/{id}")]
        [UserAuthorizationFilter(IncidentManagementRoles.AdminRole)]
        public async Task<IActionResult> Delete(Guid id)
        {
            var isDeleted = await _incidentRepository.DeleteIncident(id);
            return Ok(new { result = isDeleted });
        }

    }
}
