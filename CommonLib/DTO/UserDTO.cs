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
        public string FirstName { get; set; }
        public string Surname { get; set; }
        public int UserTypeId { get; set; } = 0;
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public float Balance { get; set; } = float.NaN;
    }
}
