using AutoMapper;
using CommonLib.DAL;
using NLog;
using NLog.Web;

namespace DataAccessGrpcService.Services;

public partial class DataAccessGrpcBase : DataAccessGrpcService.DataAccessGrpcServiceBase
{
    PostgresRepository _repository;
    IMapper _mapper;
    NotificationQueueService _notificationQueueService;
    Logger _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
    object _lockerUpdateApp = new();
    public DataAccessGrpcBase(PostgresRepository repo, IMapper mapper, NotificationQueueService notifyQueueService)
    {
        _repository = repo;
        _mapper = mapper;
        _notificationQueueService = notifyQueueService;
    }
}
