using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IncidentManagement.DataModel.Incident
{
    [Table("tblIncidentStatus")]
    public class IncidentStatusModel
    {
        [Key]
        [Required]
        [Column("Id")]
        public int IncidentStatusId { get; set; }

        [Required, MinLength(3), MaxLength(30)]
        public string StatusName { get; set; }

        [Required, MinLength(3), MaxLength(100)]
        public string StatusDescription { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

        public bool IsActive { get; set; } = true;

        public IncidentStatusModel()
        {
            CreatedOn = DateTime.UtcNow;
            ModifiedOn = DateTime.UtcNow;
        }
    }
}
