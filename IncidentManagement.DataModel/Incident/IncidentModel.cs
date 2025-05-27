using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.DataModel.Incident
{
    [Table("tblIncidents")]
    public class IncidentModel
    {
        [Key]
        [Required]
        [Column("Id")]
        public Guid IncidentId { get; set; }

        [Required, MaxLength(100)]
        public string IncidentTitle { get; set; }

        [Required, MaxLength(500)]
        public string IncidentDescription { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;
        
        public bool IsActive { get; set; } = true;

        [Required]
        public int IncidentStatusId { get; set; }

        [ForeignKey("IncidentStatusId")]
        public IncidentStatusModel IncidentStatus { get; set; }

        [Required]
        public Guid CategoryId { get; set; }
        
        [ForeignKey("CategoryId")]
        public CategoryModel Category { get; set; }
        
        public Guid CreatedBy { get; set; }
        [ForeignKey("CreatedBy")]
        
        public UserModel User { get; set; }

        public Guid AssignedToUser { get; set; }
    }
}
