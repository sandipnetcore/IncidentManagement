using IncidentManagement.BusinessLogic.Audit;
using IncidentManagement.BusinessLogic.Roles;
using IncidentManagement.DataModel.AuditLogs;
using IncidentManagement.DataModel.User;
using IncidentManagement.WebAPI.Controllers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace IncidentManagement.WebAPI.IMAttribute
{
    public class UserAuthorizationFilter : Attribute, IAuthorizationFilter, IActionFilter
    {
        private readonly IList<string> _roles;
        private UserModel _user { get; set; }

        private IAuditRepository _auditRepository;
        public UserAuthorizationFilter(params string[] Roles)
        {
            _roles = Roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            _user = context.HttpContext.Items["User"] as UserModel;
            _auditRepository = context.HttpContext.RequestServices.GetRequiredService<IAuditRepository>();

            if (_user == null)
            {
                _user = _user.GetEmptyUserModel();

                LogAudit("Unauthorized Access",
                    $"{_user.UserName} attempted to access {context.ActionDescriptor.DisplayName} without valid authentication.",
                    "Error");

                context.Result = new JsonResult(new { message = "Unauthorized. The token is not valid" })
                {
                    StatusCode = StatusCodes.Status401Unauthorized
                };
            }

            var isAuthorized = false;

            foreach (var role in _roles)
            {
                var roleFound = _user?.UserRoles?
                    .FirstOrDefault(r => r.RoleId.GetRoleName().Equals(role, StringComparison.OrdinalIgnoreCase));

                isAuthorized = roleFound != null;
            }

            if (!isAuthorized)
            {
                context.Result = new JsonResult(new { message = "Forbidden. You do not have permission to access this resource." })
                {
                    StatusCode = StatusCodes.Status403Forbidden
                };
            }
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            var cont = context.Controller as BaseController;

            if (cont != null)
            {
                cont.baseUserModel = _user;
            }

            var description = $"{cont.baseUserModel.UserName} performed {context.ActionDescriptor.DisplayName} action.";
            LogAudit(context.ActionDescriptor.DisplayName, description, "Info");
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
            var cont = context.Controller as BaseController;

            if (cont != null)
            {
                cont.baseUserModel = _user;
            }
            var description = $"{cont.baseUserModel.UserName} performed {context.ActionDescriptor.DisplayName} action.";
            LogAudit(context.ActionDescriptor.DisplayName, description, "Info");
        }

        private void LogAudit(string action, string description, string auditType)
        {
            var auditLog = new AuditLogsModel
            {
                UserId = _user.UserId,
                UserName = _user.UserName,
                Action = action,
                CreatedOn = DateTime.UtcNow,
                Description = description,
                AuditType = auditType
            };


            this._auditRepository.Save(auditLog).GetAwaiter().GetResult();
        }
    }
}
