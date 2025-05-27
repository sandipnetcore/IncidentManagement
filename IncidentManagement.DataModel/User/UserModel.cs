using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IncidentManagement.DataModel.User
{
    [Table("tblUsers")]
    public class UserModel
    {
        [Key]
        [Required]
        [Column("Id")]
        public Guid UserId { get; set; }

        [Required, MinLength(6), MaxLength(30)]
        public string FirstName { get; set; }

        [Required, MinLength(6), MaxLength(30)]
        public string LastName { get; set; }

        [Required, MinLength(6), MaxLength(12)]
        public string UserName { get; set; }

        [Required, MinLength(6), MaxLength(220)]
        public string Password { get; set; }


        [Required]
        public bool IsActive { get; set; } = true;

        [Required]
        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public DateTime ModifiedOn { get; set; } = DateTime.UtcNow;

        [Required]
        public int RoleId { get; set; }

        [ForeignKey("RoleId")]
        public RoleModel UserRole { get; set; }

        [InverseProperty("User")]
        public List<UserRoleModel> UserRoles { get; set; }
    }
}
