using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommonLib.Enums
{
    public enum AppStatusEnum
    {
        [Description("Первичная обработка")]
        PrimaryProcessing = 1,

        [Description("Направлена в службу")]
        SentToService = 2,

        [Description("Назначен исполнитель")]
        ExecutorAppointed = 3,

        [Description("В исполнении")]
        InPerformance = 4,

        [Description("Выполнена")]
        Completed = 5,

        [Description("Переоткрыта")]
        Reopened = 6,

        [Description("Закрыта")]
        Closed = 7,

        [Description("Отклонена")]
        Rejected = 8
    }
}
