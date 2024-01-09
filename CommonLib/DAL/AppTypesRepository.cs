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
        public async Task<List<ApplicationType>> GetApplicationTypes(CancellationToken cancellation = default)
        {
            return await context.AppTypes.AsNoTracking().ToListAsync(cancellation);
        }
    }
}
