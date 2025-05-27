using IncidentManagement.DataModel.Incident;
using IncidentManagement.DataModel.UIModels.UIIncident;
using IncidentManagement.DataModel.User;
using IncidentManagement.EntityFrameWork.DBOperations;
using Microsoft.EntityFrameworkCore;
using static IncidentManagement.BusinessLogic.Roles.IncidentManagementStatus;

namespace IncidentManagement.BusinessLogic.Incident
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly DataContext _dataContext;
        public IncidentRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public Task<List<UIIncidentModel>> GetAllIncidents()
        {
            List<UIIncidentModel> incidents = new List<UIIncidentModel>();
            incidents = _dataContext.Incidents.
                    Where(i => i.IsActive).
                    Select(i => new UIIncidentModel()
                    {
                        IncidentId = i.IncidentId.ToString(),
                        IncidentTitle = i.IncidentTitle,
                        IncidentDescription = i.IncidentDescription,
                        IncidentStatus = i.IncidentStatus.StatusName,
                        CategoryName = i.Category.CategoryName,
                        UserName = i.User.UserName,
                        AssignedToUser = _dataContext.Users
                            .Where(u => u.UserId == i.AssignedToUser && u.IsActive)
                            .Select(u => u.UserName)
                            .FirstOrDefault() ?? "Not Assigned",
                        CreatedOn = i.CreatedOn.ToString("dd/MMM/yyyy"),

                    }).ToList();
            return Task.FromResult(incidents);
        }

        public async Task<UIIncidentDetailsModel?> GetIncidentCompleteDetailsById(Guid incidentId)
        {
            var result = await _dataContext.Incidents.Where(x=> x.IncidentId == incidentId).Join(
                _dataContext.IncidentComments,
                incident => incident.IncidentId,
                comment => comment.IncidentId,
                (incident, comment) => new UIIncidentDetailsModel
                {
                    IncidentId = incident.IncidentId.ToString(),
                    IncidentTitle = incident.IncidentTitle,
                    IncidentDescription = incident.IncidentDescription,
                    Category = incident.Category.CategoryName,
                    CreatedBy = incident.User.UserName,
                    CreatedOn = incident.CreatedOn.ToString("dd/MMM/yyyy"),
                    IncidentStatus = incident.IncidentStatus.StatusName,
                    AssignedToUser = _dataContext.Users
                        .Where(u => u.UserId == incident.AssignedToUser && u.IsActive)
                        .Select(u => u.UserName)
                        .FirstOrDefault() ?? "Not Assigned",
                    IncidentComments = _dataContext.IncidentComments.Select(c => new UICommentModel
                    {
                        IncidentId = c.IncidentId.ToString(),
                        CommentText = c.CommentText,
                        CreatedOn = c.CreatedOn.ToString("dd/MMM/yyyy"),
                        UserName = c.User.UserName,
                    }).Where(c => c.IncidentId == incidentId.ToString() && incident.IsActive).ToList()
                }
            ).FirstOrDefaultAsync();

            return result;
        }


        public async Task<bool> CreateIncident(UIIncidentModel model, UserModel user)
        {
            bool isIncidentCreated = false;

            var incident = new IncidentModel
            {
                IncidentId = Guid.NewGuid(),
                IncidentTitle = model.IncidentTitle,
                IncidentDescription = model.IncidentDescription,
                CategoryId = Guid.Parse(model.CategoryId),
                CreatedBy = user.UserId,
                IncidentStatusId = (int)IMStatus.New,
                CreatedOn = DateTime.UtcNow,
                AssignedToUser = user.UserId, 
                IsActive = true
            };

            await _dataContext.Incidents.AddAsync(incident);

            isIncidentCreated = await ModifyIncident(incident, "Incident created by user.");

            return isIncidentCreated;
        }

        public async Task<bool> AddUserAndCommentsToIncident(UICommentModel model)
        {
            var isCommentAdded = false;

            var incident = _dataContext.Incidents.FirstOrDefault(i=> i.IncidentId == Guid.Parse(model.IncidentId) 
                                                            && i.IsActive);
            var assignedto = _dataContext.Users
                .FirstOrDefault(u => u.UserName == model.AssignedToUser && u.IsActive);

            if (incident != null && assignedto != null)
            {
                incident.AssignedToUser = assignedto?.UserId ?? incident.AssignedToUser;
                _dataContext.Update<IncidentModel>(incident);
                isCommentAdded = await AddCommentToIncident(incident.IncidentId, model.CommentText, incident.User.UserId);
            }
            
            return isCommentAdded;
        }

        private async Task<bool> AddCommentToIncident(Guid incidentId, string commentText, Guid userId)
        {
            var isCommentAdded = false;
            try
            {
                var model = new IncidentCommentModel
                {
                    IncidentId = incidentId,
                    CommentText = commentText,
                    CreatedBy = userId,
                    CreatedOn = DateTime.UtcNow
                };
                await _dataContext.IncidentComments.AddAsync(model);

                _dataContext.SaveChanges();
                isCommentAdded = true;
            }
            catch (Exception ex)
            {
                throw new Exception($"Error adding comment: {ex.Message}", ex);
            }

            return isCommentAdded;
        }

        public async Task<bool> ChangeIncidentStatus(Guid id, int statusId)
        {
            var isStatusChanged = false;

            var incident =
                    _dataContext.Incidents
                    .FirstOrDefault(i => i.IncidentId == id);


            if (incident != null)
            {
                incident.IncidentStatusId = statusId;
                incident.ModifiedOn = DateTime.UtcNow;
                _dataContext.Update<IncidentModel>(incident);

                isStatusChanged = await ModifyIncident(incident, $"Incident status changed to {statusId}");
            }

            return isStatusChanged;
        }

        public async Task<bool> CloseIncident(Guid id)
        {
            var isClosed = false;
            var model = _dataContext.Incidents
                .FirstOrDefault(i => i.IncidentId == id && i.IsActive);

            if (model != null)
            {
                model.IsActive = false;
                _dataContext.Update<IncidentModel>(model);
                isClosed = await ModifyIncident(model, "Incident closed by user.");
            }

            return isClosed;
        }

        public Task<bool> DeleteIncident(Guid id)
        {
            var isDeleted = false;
            try
            {
                var model = _dataContext.Incidents
                    .FirstOrDefault(i => i.IncidentId == id && i.IsActive);

                if (model != null)
                {
                    model.IsActive = false;
                    model.ModifiedOn = DateTime.UtcNow;
                    _dataContext.Update(model);
                    _dataContext.SaveChanges();
                    isDeleted = true;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message, ex);
            }

            return Task.FromResult(isDeleted);
        }

        private async Task<bool> ModifyIncident(IncidentModel incidentModel, string commentText)
        {
            var isCompleted = false;

            if (incidentModel != null)
            {
                isCompleted = await AddCommentToIncident(incidentModel.IncidentId, commentText, incidentModel.User.UserId);
            }

            return isCompleted;
        }

        public async Task<List<UIIncidentStatusModel>> GetAllStatusTypes()
        {
            return await _dataContext.IncidentStatuses
                .Where(s => s.IsActive)
                .Select(s => new UIIncidentStatusModel
                {
                    StatusId = s.IncidentStatusId,
                    StatusName = s.StatusName
                }).ToListAsync();
        }
    }
}
