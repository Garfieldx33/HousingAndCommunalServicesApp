using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Enums
{
    public enum AppTypeEnum
    {
        [Description("Жалоба")]
        Complaint = 1,

        [Description("Авария")]
        Accident = 2,

        [Description("Платные услуги")]
        PaidServices = 3,

        [Description("Плановые работы")]
        ScheduledWorks = 4,
    }
}
