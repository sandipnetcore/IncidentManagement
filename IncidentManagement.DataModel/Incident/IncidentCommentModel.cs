using IncidentManagement.DataModel.User;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentManagement.DataModel.Incident
{
    [Table("tblIncidentComments")]
    public class IncidentCommentModel
    {
        [Key]
        [Required]
        [Column("Id")]
        public Guid CommentId { get; set; }
        
        [Required, MinLength(3), MaxLength(500)]
        public string CommentText { get; set; }
        
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        
        [Required]
        public Guid IncidentId { get; set; }
        
        [ForeignKey("IncidentId")]
        public IncidentModel Incident { get; set; }
        
        
        [Required]
        [ForeignKey("UserId")]
        public Guid CreatedBy { get; set; }
        
        [ForeignKey("CreatedBy")]
        public UserModel User { get; set; }
    }
}
