using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CommonLib.Entities
{
    [Table("application")]
    public class Application
    {
        [Column("id")]
        [ForeignKey(nameof(Application.Id))]
        public int Id { get; set; }

        [Column("name")]
        public string Subject { get; set; }

        [Column("description")]
        public string Description { get; set; }

        [Column("status")]
        [ForeignKey(nameof(Application.StatusId))]
        public int StatusId { get; set; }

        [Column("type")]
        public int ApplicationTypeId { get; set; }

        [Column("department")]
        [ForeignKey(nameof(Application.DepartmentId))]
        public int? DepartmentId { get; set; }

        [Column("applicant")]
        [ForeignKey(nameof(Application.ApplicantId))]
        public int ApplicantId { get; set; }

        [Column("executor")]
        public int? ExecutorId { get; set; }
        
        [Column("datecreate")]
        public DateTimeOffset DateCreate { get; set; }
        
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
