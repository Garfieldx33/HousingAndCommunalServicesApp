using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DTO
{
    public class UpdateAppDto
    {
        public int Id { get; set; }
        public int Status { get; set; } = 0;
        public int ApplicationTypeId { get; set; }
        public int DepartmentId { get; set; }
        public int ExecutorId { get; set; }
        public DateTime DateConfirm { get; set; }
        public DateTime DateClose { get; set; }
    }
}
