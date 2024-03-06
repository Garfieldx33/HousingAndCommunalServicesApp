namespace CommonLib.DTO;

public class ApplicationDTO
{
    public string Subject { get; set; }
    public string Description { get; set; }
    public int StatusId { get; set; } = 1;
    public int ApplicationTypeId { get; set; }
    public int DepartmentId { get; set; }
    public int ApplicantId { get; set; }
    public DateTime DateCreate { get; set; }
}
