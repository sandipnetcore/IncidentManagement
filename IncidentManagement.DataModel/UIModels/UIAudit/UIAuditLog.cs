
namespace IncidentManagement.DataModel.UIModels.UIAudit
{
    public class UIAuditLog
    {
        public string Action { get; set; }

        public string Description { get; set; }

        public DateTime CreatedOn { get; set; }

        public Guid UserId { get; set; }

        public string UserName { get; set; }

        public string AuditType { get; set; }
    }
}
