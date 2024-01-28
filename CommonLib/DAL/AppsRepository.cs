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

        public async Task<Application> UpdateApplicationAsync(Application appUpdate, CancellationToken cancellation = default)
        {
            Application? updatedApp;
            if (appUpdate.Id > 0)
            {
                updatedApp = context.Apps.Single(q => q.Id == appUpdate.Id); 
                if (updatedApp != null)
                {
                    context.Apps.Update(appUpdate);
                    await context.SaveChangesAsync(cancellation);
                }
            }
            return context.Apps.Single(a => a.Id == appUpdate.Id);
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
