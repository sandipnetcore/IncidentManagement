using System.ComponentModel.DataAnnotations;

namespace IncidentManagement.DataModel.UIModels.UIIncident
{
    public class UIIncidentModel
    {
        public string IncidentId { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string IncidentTitle { get; set; }


        [Required, MinLength(10), MaxLength(500)]
        public string IncidentDescription { get; set; }

        public string IncidentStatus { get; set; }

        public string CategoryId { get; set; }
        
        public string CategoryName { get; set; }

        public string UserName { get; set; }

        public string AssignedToUser { get; set; }

        public string CreatedOn { get; set; }

    }
}
