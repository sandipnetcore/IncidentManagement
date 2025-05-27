namespace IncidentManagement.BusinessLogic.Roles
{
    public static class IncidentManagementRoles
    {
        /// <summary>
        /// Performs only admin roles.
        /// </summary>
        public const string AdminRole = "Admin";


        /// <summary>
        /// Performs only the User roles
        /// </summary>
        public const string User = "User";

        public enum IMRoles 
        { 
            AdminRole = 1, 
            User  = 2
        }

        public static string GetRoleName (this int roleId)
        {
            var roles = (IMRoles)roleId;

            switch (roles)
            {
                case IMRoles.AdminRole: return AdminRole;
                default: return User;
            }
        }
    }
}
