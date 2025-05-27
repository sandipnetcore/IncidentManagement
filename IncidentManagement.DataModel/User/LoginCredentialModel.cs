using System.ComponentModel.DataAnnotations;

namespace IncidentManagement.DataModel.User
{
    public class LoginCredentialModel
    {
        [Required, MinLength(6), MaxLength(12)]
        public string UserName { get; set; }

        [Required, MinLength(6), MaxLength(12)]
        public string Password { get; set; }
    }
}
