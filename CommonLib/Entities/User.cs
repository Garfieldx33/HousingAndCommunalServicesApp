using CommonLib.Helpers;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace CommonLib.Entities
{
    [Table("user")]
    public class User
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string FirstName { get; set; }
        
        [Column("surname")]
        public string? SecondName { get; set; }

        [Column("dateofbirth")]
        public DateTime? DateOfBirth { get; set; }

        [Column("address")]
        public string? Address { get; set; }

        [Column("phone")]
        public string? Phone { get; set; }

        [Column("email")]
        public string? Email { get; set; }

        [Column("registrationdate")]
        public DateTime DateRegistration { get; set; }

        [Column("type")]
        public int Type { get; set; }

        [Column("login")]
        public string Login { get; set; }

        [Column("password")]
        public string Password { get; set; }

        public override string ToString()
        {
            return @$"Пользователя: Name = {FirstName};";
        }
    }
}
