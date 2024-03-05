using CommonLib.DTO;
using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;

namespace CommonLib.DAL
{
    public partial class PostgresRepository
    {
        public async Task<List<Application>> GetApplicationByApplicantId(int UserId, CancellationToken cancellation = default)
        {
            var query = context.Apps.AsNoTracking();
            if (UserId > 0)
            {
                query = query.Where(q => q.ApplicantId == UserId);
            }
            return await query.ToListAsync(cancellation);
        }

        public async Task<int> AddNewApplication(Application newApplication, CancellationToken cancellation = default)
        {
            await context.Apps.AddAsync(newApplication, cancellation);
            return context.SaveChanges();
        }

        public async Task<Application> UpdateApplicationAsync(UpdateAppDto request, CancellationToken cancellation = default)
        {
            if (request.Id > 0)
            {
                Application updatedApp = context.Apps.Single(q => q.Id == request.Id);
                if (updatedApp != null)
                {
                    if (request.ApplicationTypeId > 0)
                        { updatedApp.ApplicationTypeId = (Enums.AppTypeEnum)request.ApplicationTypeId; }
                    if (request.Status > 0)
                        { updatedApp.Status = (Enums.AppStatusEnum)request.Status; }
                    if (request.DepartmentId > 0)
                        { updatedApp.DepartmentId = request.DepartmentId; }
                    if (request.ExecutorId > 0)
                        { updatedApp.ExecutorId = request.ExecutorId; }
                    if (request.DateClose != DateTime.MinValue)
                        { updatedApp.DateClose = request.DateClose; }
                    if (request.DateConfirm != DateTime.MinValue)
                        { updatedApp.DateConfirm = request.DateConfirm; }

                    context.Apps.Update(updatedApp);
                    await context.SaveChangesAsync(cancellation);
                }
            }
            return context.Apps.Single(q => q.Id == request.Id);
        }

        public async Task<int> DeleteApplicationAsync(int appId, CancellationToken cancellation = default)
        {
            if (appId > 0)
            {
                var delApp = context.Apps.Single(q => q.Id == appId);
                if (delApp != null)
                {
                    context.Apps.Remove(delApp);
                    return await context.SaveChangesAsync(cancellation);
                }
            }
            return await Task.FromResult(0);
        }
    }
}
