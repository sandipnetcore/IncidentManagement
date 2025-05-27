using IncidentManagement.DataModel.AuditLogs;
using IncidentManagement.DataModel.Category;
using IncidentManagement.DataModel.Incident;
using IncidentManagement.DataModel.User;
using Microsoft.EntityFrameworkCore;

namespace IncidentManagement.EntityFrameWork.DBOperations
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {
        }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<RoleModel> Roles { get; set; }
        public DbSet<UserRoleModel> UserRoles { get; set; }
        
        public DbSet<IncidentModel> Incidents { get; set; }
        public DbSet<IncidentStatusModel> IncidentStatuses { get; set; }
        public DbSet<IncidentCommentModel> IncidentComments { get; set; }

        public DbSet<CategoryModel> Categories { get; set; }
        
        public DbSet<AuditLogsModel> AuditLogs { get; set; }

    }
}
