
using IncidentManagement.DataModel.AuditLogs;
using IncidentManagement.DataModel.UIModels.UIAudit;

namespace IncidentManagement.BusinessLogic.Audit
{
    public interface IAuditRepository
    {
        Task<List<UIAuditLog>> GetAllAudits();

        Task Save(AuditLogsModel auditLog);
    }
}
