using System.ComponentModel;

namespace CommonLib.Enums;

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
