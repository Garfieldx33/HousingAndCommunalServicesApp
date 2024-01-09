using CommonLib.DTO;
using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DAL
{
    public partial class PostgresRepository
    {
        public async Task<List<Application>> GetApplicationByUserId(int UserId, CancellationToken cancellation = default)
        {
            var query = context.Apps.AsNoTracking();
            if (UserId > 0)
            {
                query = query.Where(q => q.Applicant == UserId);
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
