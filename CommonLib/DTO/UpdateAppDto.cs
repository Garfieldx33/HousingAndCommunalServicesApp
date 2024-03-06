namespace CommonLib.DTO;

public class UpdateAppDTO
{
    public int Id { get; set; }
    public int Status { get; set; } = 0;
    public int ApplicationTypeId { get; set; }
    public int DepartmentId { get; set; }
    public int ExecutorId { get; set; }
    public DateTime DateConfirm { get; set; }
    public DateTime DateClose { get; set; }
}
