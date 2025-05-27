using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentManagement.DataModel.UIModels.UIIncident
{
    public class UIIncidentDetailsModel
    {
        public string IncidentId { get; set; }
        public string IncidentTitle { get; set; }
        public string IncidentDescription { get; set; }
        public string Category { get; set; }
        public string CreatedBy { get; set; }
        public string CreatedOn { get; set; }

        public string AssignedToUser { get; set; }
        public string IncidentStatus { get; set; }
        public List<UICommentModel> IncidentComments { get; set; }
    }
}
