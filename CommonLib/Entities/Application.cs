using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CommonLib.Entities
{
    [Table("application")]
    public class Application 
    {
        [Column("id")]
        public int Id { get; set; }
        
        [Column("name")]
        public string Subject { get; set; }
        
        [Column("description")]
        public string Description { get; set; }
        
        [Column("status")]
        public int Status { get; set; }

        [Column("type")]
        public int Type { get; set; }

        [Column("department")]
        public int? Department { get; set; }

        [Column("applicant")]
        public int Applicant { get; set; }

        [Column("executor")]
        public int? Executor { get; set; }
        
        [Column("datecreate")]
        public DateTime DateCreate { get; set; }
        
        [Column("dateconfirm")]
        public DateTime? DateConfirm { get; set; }
        
        [Column("dateclose")]
        public DateTime? DateClose { get; set; }

        public override string ToString()
        {
            return @$"Заявка: Id = {Id}; Кратко {Subject}";
        }
    }
}
