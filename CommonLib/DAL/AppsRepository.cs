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

    }
}
