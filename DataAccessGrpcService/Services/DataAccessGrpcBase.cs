using AutoMapper;
using CommonLib.DAL;
using CommonLib.Entities;
using Grpc.Core;
using NLog;
using NLog.Web;

namespace DataAccessGrpcService.Services
{
    public partial class DataAccessGrpcBase : DataAccessGrpcService.DataAccessGrpcServiceBase
    {
        PostgresRepository _repository;
        IMapper _mapper;
        NotificationQueueService _notificationQueueService;
        Logger _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        public DataAccessGrpcBase(PostgresRepository repo, IMapper mapper, NotificationQueueService notifyQueueService)
        {
            _repository = repo;
            _mapper = mapper;
            _notificationQueueService = notifyQueueService;
        }
    }
}
