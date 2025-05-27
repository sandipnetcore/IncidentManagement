using IncidentManagement.DataModel.User;
using System.IdentityModel.Tokens.Jwt;

namespace IncidentManagement.BusinessLogic.User
{
    public interface IUserRepository
    {
        Task<UserModel?> GetUserByUserName(string UserName);
        Task<string?> GenerateToken(LoginCredentialModel loginCredentialsModel);

        Task<JwtSecurityToken?> ValidateToken(string RequestToken);
    }
}
