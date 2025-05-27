
using IncidentManagement.DataModel.AuditLogs;
using IncidentManagement.DataModel.UIModels.UIAudit;
using IncidentManagement.EntityFrameWork.DBOperations;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.BusinessLogic.Audit
{
    public class AuditRepository: IAuditRepository
    {
        private readonly DataContext _dataContext;
        
        public AuditRepository(DataContext dataContext) 
        {
            _dataContext = dataContext;
        }

        public async Task<List<UIAuditLog>> GetAllAudits()
        {
            var auditLogs =  await _dataContext.AuditLogs.Select(a => new UIAuditLog()
            {
                Action = a.Action,
                Description = a.Description,
                CreatedOn = a.CreatedOn,
                UserId = a.UserId,
                UserName = a.UserName, 
                AuditType = a.AuditType
            }).ToListAsync();

            return auditLogs;
        }

        public async Task Save(AuditLogsModel auditLog)
        {
            try
            {
                _dataContext.AuditLogs.Add(auditLog);
                await _dataContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Error saving audit log: {ex.Message}", ex);
            }
        }
    }
}
