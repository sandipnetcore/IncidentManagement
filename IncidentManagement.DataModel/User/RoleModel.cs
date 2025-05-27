using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.DataModel.User
{
    [Table("tblRoles")]
    public class RoleModel
    {
        [Key]
        [Column("Id")]
        public int RoleId { get; set; }

        [Required]
        public string RoleName { get; set; }

        public bool IsActive { get; set; } = true;

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public DateTime ModifiedOn { get;set; } = DateTime.UtcNow;

        public List<UserModel> Users { get; set; }

        [InverseProperty("Role")]
        public List<UserRoleModel> UserRoles { get; set; }
    }
}
