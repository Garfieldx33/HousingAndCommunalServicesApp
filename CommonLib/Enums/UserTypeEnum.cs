using System.ComponentModel;

namespace CommonLib.Enums;

public enum UserTypeEnum
{
    [Description("Неизвестно")]
    Unknown = 0,

    [Description("Администратор")]
    Administrator = 1,

    [Description("Работник")]
    Employee = 2,

    [Description("Житель")]
    Inhabitant = 3
}
