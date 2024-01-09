using System.ComponentModel.DataAnnotations;

namespace CommonLib.DTO
{
    public class ApplicationDTO
    {
        [Required(ErrorMessage = "Необходимо указать тему.")]
        [Display(Name = "Тема")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Необходимо описать вопрос.")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        public int StatusId { get; set; } = 1;
        public int TypeId { get; set; }
        public int ApplicantId { get; set; } 
        
        public int ApplicationTypeId { get; set; }

        public int DepartamentId { get; set; }
        public DateTimeOffset DateCreate { get; set; }

    }
}
