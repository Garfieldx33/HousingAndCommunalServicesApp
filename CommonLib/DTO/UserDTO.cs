using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.DTO
{
    public class UserDTO
    {
        public int? UserId { get; set; }
        public string? FirstName { get; set; }
        public int UserTypeId { get; set; } = 0;
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
    }
}
