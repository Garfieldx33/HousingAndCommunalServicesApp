using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DAL
{
    public partial class PostgresRepository
    {
        private AppDbContext context { get; set; }
        public PostgresRepository(AppDbContext context) 
        { 
            this.context = context;
        }
    }
}
