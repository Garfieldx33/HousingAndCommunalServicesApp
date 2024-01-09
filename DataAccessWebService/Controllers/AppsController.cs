using AutoMapper;
using CommonLib.DAL;
using CommonLib.DTO;
using CommonLib.Entities;
using Microsoft.AspNetCore.Mvc;

namespace DataAccessWebService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AppsController : ControllerBase
    {
        PostgresRepository _repository;
        IMapper _mapper;
        public AppsController(PostgresRepository repo, IMapper mapper)
        {
            _repository = repo;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<Application>> GetApps(int applicantId)
        {
            return await _repository.GetApplicationByUserId(applicantId);
        }

        [HttpPost]
        public async Task<int> AddNewApp(ApplicationDTO applicationDTO)
        {
            Application newApp = _mapper.Map<Application>(applicationDTO);
            newApp.DateCreate = DateTimeOffset.Parse(newApp.DateCreate.ToString());
            return await _repository.AddNewApplication(newApp);
        }
    }
}
