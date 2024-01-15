using AutoMapper;
using CommonLib.DAL;
using CommonLib.DTO;
using Grpc.Core;

namespace DataAccessGrpcService.Services
{
    public class DataAccessGrpc : DataAccessGrpcService.DataAccessGrpcServiceBase
    {
        PostgresRepository _repository;
        IMapper _mapper;
        public DataAccessGrpc(PostgresRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public override async Task<GetAppsByUserIdResponce> GetAppsByUserId(GetAppsByUserIdRequest request, ServerCallContext context)
        {
            var result = await _repository.GetApplicationByApplicantId(request.ApplicantId);
            GetAppsByUserIdResponce r = new();
            result.ForEach(i => r.Applications.
            Add(new Application { 
                ApplicantId = i.ApplicantId,
                ApplicationTypeId = (int)i.ApplicationTypeId,
                Subject = i.Subject,
            }));
            //r.Applications.AddRange((IEnumerable<Application>)result);
            return r;
        }

        /*public override Task<AddNewAppResponce> AddNewApp(AddNewAppRequest request, ServerCallContext context)
        {
            Application newApp = _mapper.Map<Application>((CommonLib.DTO.ApplicationDTO)request.ApplicationDto);
            var res = await _repository.AddNewApplication(newApp); 
            return new AddNewAppResponce { ResultOfInsert = 1 };
        }*/
    }
}
