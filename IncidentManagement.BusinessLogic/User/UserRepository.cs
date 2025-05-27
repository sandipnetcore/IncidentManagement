

using IncidentManagement.BusinessLogic.Configuration;
using IncidentManagement.BusinessLogic.Roles;
using IncidentManagement.DataModel.User;
using IncidentManagement.EntityFrameWork.DBOperations;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace IncidentManagement.BusinessLogic.User
{
    public class UserRepository : IUserRepository
    {
        private DataContext _dataContext { get; }
        private JWTConfigurationSettings _JWTConfigurationSettings { get; }

        private SymmetricSecurityKey PrivateSymmetricKey
        {
            get
            {
                return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_JWTConfigurationSettings.PrivateKey));
            }
        }

        public UserRepository(DataContext dataContext, IOptions<JWTConfigurationSettings> jwtConfig) 
        {
            _dataContext = dataContext;
            _JWTConfigurationSettings = jwtConfig.Value;
        }

        public async Task<UserModel?> GetUserByUserName(string UserName)
        {
            var users = _dataContext.Users
                    .Include(user => user.UserRoles)
                    .Where(user => user.UserName == UserName && user.IsActive == true)
                    .FirstOrDefault();

            return await Task.FromResult<UserModel?>(users);
        }

        public async Task<string?> GenerateToken(LoginCredentialModel loginCredentials)
        {
            try
            {
                var user = GetUserByLoginCredential(loginCredentials);

                if (user == null)
                {
                    return null;
                }

                var claimsList = GetClaims(user);

                var token = new JwtSecurityTokenHandler().WriteToken(_GenerateToken(claimsList));

                return await Task.FromResult<string?>(token);
            }
            catch
            {
                throw;
            }

        }

        public async Task<JwtSecurityToken?> ValidateToken(string RequestToken)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                tokenHandler.ValidateToken(RequestToken, new TokenValidationParameters
                {
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = PrivateSymmetricKey,
                    ClockSkew = TimeSpan.Zero,
                    ValidIssuer = _JWTConfigurationSettings.IssuerUrl,
                    ValidAudience = _JWTConfigurationSettings.AudienceUrl
                }, out SecurityToken securityToken);

                if (securityToken == null)
                {
                    return null;
                }

                var token = (JwtSecurityToken)securityToken;
                return await Task.FromResult<JwtSecurityToken?>(token);
            }
            catch
            {
                throw;
            }
        }

        private UserModel? GetUserByLoginCredential(LoginCredentialModel model)
        {
            var userModel = _dataContext.Users
                .Include(user => user.UserRoles)
                .FirstOrDefault(user => user.UserName == model.UserName 
                            && user.Password == model.Password 
                            && user.IsActive == true);

            return userModel;
        }

        private List<Claim> GetClaims(UserModel user)
        {
            var claimsList = new List<Claim>()
                {
                    new Claim("FirstName", user.FirstName),
                    new Claim("LastName", user.LastName),
                    new Claim("UserName", user.UserName.ToString()),
                };

            foreach (var role in user.UserRoles)
            {
                claimsList.Add(new Claim(ClaimTypes.Role, role.RoleId.GetRoleName()));
            }

            return claimsList;
        }
        
        private JwtSecurityToken _GenerateToken(List<Claim> claims)
        {
            var token = new JwtSecurityToken(
                    issuer: _JWTConfigurationSettings.IssuerUrl,
                    audience: _JWTConfigurationSettings.AudienceUrl,
                    expires: DateTime.Now.AddMinutes(_JWTConfigurationSettings.ExpiryInSeconds),
                    claims: claims,
                    signingCredentials: new SigningCredentials(PrivateSymmetricKey, SecurityAlgorithms.HmacSha256)
                    );

            return token;
        }
    }
}
