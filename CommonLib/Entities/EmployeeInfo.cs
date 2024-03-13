using System.ComponentModel.DataAnnotations;

namespace CommonLib.Entities;

public class EmployeeInfo
{
    [Key]
    public int EmployeeUserId { get; set; }
    public int DepartmentId { get; set; }
    public string Position { get; set; }
}
