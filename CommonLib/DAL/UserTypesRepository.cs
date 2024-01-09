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
        public async Task<List<UserType>> GetUserTypes(CancellationToken cancellation = default)
        {
            return await context.UserTypes.AsNoTracking().ToListAsync(cancellation);
        }
    }
}
