using IncidentManagement.DataModel.User;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace IncidentManagement.WebAPI.Controllers
{
    public class BaseController : ControllerBase
    {
        public UserModel baseUserModel { get; set; }
    }
}
