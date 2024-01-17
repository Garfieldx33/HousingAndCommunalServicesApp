using CommonLib.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace CommonLib.DAL
{
    public partial class PostgresRepository
    {
        public async Task<List<Department>> GetAllDepartamentsAsync(CancellationToken cancellation = default)
        {
            return await context.Departaments.AsNoTracking().ToListAsync(cancellation);
        }

        public async Task<List<Department>> GetDepartamentByIdAsync(int departamentId,  CancellationToken cancellation = default)
        {
            var query = context.Departaments.AsNoTracking();
            if (departamentId > 0)
            {
                query = query.Where(q => q.Id == departamentId);
            }
            return await query.ToListAsync(cancellation);
        }
    }
}
