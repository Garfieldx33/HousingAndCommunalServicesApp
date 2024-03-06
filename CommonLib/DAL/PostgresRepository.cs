using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DAL;

public partial class PostgresRepository
{
    private readonly AppDbContext _context;
    public PostgresRepository(AppDbContext context) 
    { 
        _context = context;
    }
}
