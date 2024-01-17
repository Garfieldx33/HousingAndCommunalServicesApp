using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Services.Models
{
    public class InvoiceDataResponce
    {
        public string Fio { get; set; }
        public string Address { get; set; }
        public decimal InvoiceSum { get; set; }
    }
}
