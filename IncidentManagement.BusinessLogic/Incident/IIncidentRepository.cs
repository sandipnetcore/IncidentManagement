using IncidentManagement.DataModel.Incident;
using IncidentManagement.DataModel.UIModels.UIIncident;
using IncidentManagement.DataModel.User;

namespace IncidentManagement.BusinessLogic.Incident
{
    public interface IIncidentRepository
    {
        Task<List<UIIncidentModel>> GetAllIncidents();

        Task<UIIncidentDetailsModel?> GetIncidentCompleteDetailsById(Guid incidentId);

        Task<Boolean> CreateIncident(UIIncidentModel model, UserModel userModel);
        Task<Boolean> AddUserAndCommentsToIncident(UICommentModel uICommentModel);
        Task<Boolean> ChangeIncidentStatus(Guid id, int statusId);
        Task<Boolean> CloseIncident(Guid id);
        Task<Boolean> DeleteIncident(Guid id);

        Task<List<UIIncidentStatusModel>> GetAllStatusTypes();
    }
}
