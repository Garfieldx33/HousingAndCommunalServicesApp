using AutoMapper;
using CommonLib.DAL;
using CommonLib.DTO;
using CommonLib.Entities;
using Grpc.Core;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessGrpcService.Services
{
    public class DataAccessService : Greeter.GreeterBase
    {
        PostgresRepository _repository;
        IMapper _mapper;
        public DataAccessService(PostgresRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Application>> GetAppsByUserId(int applicantId)
        {
            return await _repository.GetApplicationByApplicantId(applicantId);
        }

        public async Task<int> AddNewApp(ApplicationDTO applicationDTO)
        {
            Application newApp = _mapper.Map<Application>(applicationDTO);
            newApp.DateCreate = DateTimeOffset.Parse(newApp.DateCreate.ToString());
            return await _repository.AddNewApplication(newApp);
        }
    }
}
