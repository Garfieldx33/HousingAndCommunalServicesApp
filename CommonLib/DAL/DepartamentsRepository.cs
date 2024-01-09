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
        public async Task<List<Department>> GetDepartaments(CancellationToken cancellation = default)
        {
            return await context.Departaments.AsNoTracking().ToListAsync(cancellation);
        }
    }
}
