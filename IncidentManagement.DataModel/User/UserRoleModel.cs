
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.DataModel.User
{
    [Table("tblUserRoles")]
    public class UserRoleModel
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey("UserId")]
        public UserModel User { get; set; }

        [Required]
        [ForeignKey("RoleId")]
        public RoleModel Role { get; set; }

        public Guid UserId { get; set; }

        [Required]
        public int RoleId { get; set; }


    }
}
