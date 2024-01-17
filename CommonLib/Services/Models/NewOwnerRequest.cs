using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Services.Models
{
    public class NewOwnerRequest
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string BirthDate { get; set; }
        public string Address { get; set; }
        public string Inn { get; set; }
        public string Snils { get; set; }
    }
}
