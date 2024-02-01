using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Enums
{
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
}
