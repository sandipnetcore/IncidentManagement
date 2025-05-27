using IncidentManagement.DataModel.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.DataModel.AuditLogs
{
    [Table("tblAuditLogs")]
    public class AuditLogsModel
    {
        [Key]
        [Required]
        [Column("Id")]
        public Guid AuditLogId { get; set; }


        [Required, MinLength(3), MaxLength(100)]
        public string Action { get; set; }
        
        [Required, MinLength(3), MaxLength(500)]
        public string Description { get; set; }
        
        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        public string AuditType { get; set; }
    }
}
