using System.ComponentModel;

namespace CommonLib.Enums;

public enum AppTypeEnum
{
    [Description("Неизвествно")]
    Unknown = 0,

    [Description("Жалоба")]
    Complaint = 1,

    [Description("Авария")]
    Accident = 2,

    [Description("Платные услуги")]
    PaidServices = 3,

    [Description("Плановые работы")]
    ScheduledWorks = 4,

}
