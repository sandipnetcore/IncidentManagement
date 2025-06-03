using IncidentManagement.DataModel.Incident;
using IncidentManagement.DataModel.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.DataModel.Category
{

    [Table("tblCategories")]
    public class CategoryModel
    {
        
        [Key]
        [Required]
        [Column("Id")]
        public Guid CategoryId { get; set; }
        
        [Required, MinLength(3), MaxLength(30)]
        public string CategoryName { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string CategoryDescription { get; set; }

        [Required]
        public bool IsActive { get; set; } = true;
        
        [Required]
        public DateTime CreatedOn { get; set; }
        
        public DateTime ModifiedOn { get; set; }

        [Required]
        public Guid CreatedBy { get; set; }

        [ForeignKey("CreatedBy")]
        public UserModel User { get; set; }

        public Guid ModifiedBy { get; set; }

        public List<IncidentModel> Incidents { get; set; }

    }
}
