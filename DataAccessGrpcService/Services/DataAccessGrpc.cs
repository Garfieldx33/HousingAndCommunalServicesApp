using AutoMapper;
using CommonLib.DAL;
using CommonLib.DTO;
using CommonLib.Entities;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.Extensions.Logging;
using NLog;
using NLog.Web;

namespace DataAccessGrpcService.Services
{
    public class DataAccessGrpc : DataAccessGrpcService.DataAccessGrpcServiceBase
    {
        PostgresRepository _repository;
        IMapper _mapper;
        Logger _logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
        public DataAccessGrpc(PostgresRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public override async Task<GetAppsByUserIdResponce> GetAppsByUserId(GetAppsByUserIdRequest request, ServerCallContext context)
        {
            GetAppsByUserIdResponce responce = new();
            try
            {
                var result = await _repository.GetApplicationByApplicantId(request.ApplicantId);
                result.ForEach(i => responce.Applications.Add(_mapper.Map<ApplicationGrpc>(i)));
                //result.ForEach(i => responce.Applications.Add(MapApp(i)));
            }
            catch (Exception ex) 
            {
                _logger.Warn(ex);
            }
            
            return responce;
        }

        public override async Task<AddNewAppResponce> AddNewApp(AddNewAppRequest request, ServerCallContext context)
        {
            Application newApp = _mapper.Map<Application>(request.ApplicationDto);
            var res = await _repository.AddNewApplication(newApp); 
            return new AddNewAppResponce { ResultOfInsert = res };
        }

        /*static ApplicationGrpc MapApp(Application app)
        {
            return new ApplicationGrpc
            {
                ApplicantId = app.ApplicantId,
                ApplicationTypeId = (int)app.ApplicationTypeId,
                DateClose = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)(app.DateClose != null ? app.DateClose : DateTime.Parse("1970-01-01")), DateTimeKind.Utc)),
                DateConfirm = Timestamp.FromDateTime(DateTime.SpecifyKind((DateTime)(app.DateConfirm != null ? app.DateConfirm : DateTime.Parse("1970-01-01")), DateTimeKind.Utc)),
                DateCreate = Timestamp.FromDateTime(DateTime.SpecifyKind(app.DateCreate, DateTimeKind.Utc)),
                DepartamentId = (int)(app.DepartamentId != null ? app.DepartamentId : 0),
                Description = app.Description,
                ExecutorId = app.ExecutorId,
                Id = app.Id,
                Status = (int)app.Status,
                Subject = app.Subject
            };
        }*/
    }
}
