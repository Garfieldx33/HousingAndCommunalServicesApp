using CommonLib.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;

namespace CommonLib.Entities
{
    public class Application
    {
        [Key]
        public int Id { get; set; }
        public string Subject { get; set; }
        public string Description { get; set; }
        public AppStatusEnum Status { get; set; }
        public AppTypeEnum ApplicationTypeId { get; set; }
        public int? DepartamentId { get; set; }
        public int ApplicantId { get; set; }
        public int? ExecutorId { get; set; }
        public DateTime DateCreate { get; set; }
        public DateTime? DateConfirm { get; set; }
        public DateTime? DateClose { get; set; }
    }
}
