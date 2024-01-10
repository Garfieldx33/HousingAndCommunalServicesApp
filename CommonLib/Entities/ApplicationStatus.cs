using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Entities
{
    public class ApplicationStatus
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
