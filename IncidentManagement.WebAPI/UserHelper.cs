using IncidentManagement.DataModel.User;

namespace IncidentManagement.WebAPI
{
    public static class UserHelper
    {
        public static UserModel GetEmptyUserModel(this UserModel userModel)
        {
            if (userModel == null)
            {
                return new UserModel
                {
                    UserId = Guid.NewGuid(),
                    UserName = "Anonymous",
                };
            }

            return userModel;
        }
    }
}
