using IncidentManagement.DataModel.User;

namespace IncidentManagement.DataModel.Incident
{
    public class IncidentCompleteDetailsModel: IncidentModel
    {
        public List<IncidentCommentModel> IncidentComments { get; set; }
    }
}
