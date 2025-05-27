namespace IncidentManagement.BusinessLogic.Roles
{
    public static class IncidentManagementStatus
    {
        public const string New = "New";

        public const string InProgress = "In Progress";

        public const string Resolved = "Resolved";
        public enum IMStatus
        {
            New = 1,
            InProgress = 2,
            Resolved = 3
        }

        public static string GetStatusName (this int statusId)
        {
            var roles = (IMStatus)statusId;

            switch (roles)
            {
                case IMStatus.Resolved: return Resolved;
                case IMStatus.InProgress: return InProgress;
                default: return New;
            }
        }
    }
}
