using CommonGrpcService.Services;
using CommonLib.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CommonGrpcService.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class AdministratorController : ControllerBase
    {
        AdministratorGrpcService _administratorGrpcService;

        public AdministratorController(AdministratorGrpcService administratorGrpcService)
        {
            _administratorGrpcService = administratorGrpcService;
        }

        [HttpGet]
        public async Task<ResidentDTO> GetOwnerById(long ownerId)
        {
            return _administratorGrpcService.GetOwnerById(ownerId);
        }

        [HttpGet]
        public async Task<StaffDTO> GetStaffById(long staffId)
        {
            return _administratorGrpcService.GetStaffById(staffId);
        }

        [HttpGet]
        public async Task<ApplicationDTO> GetApplicationById(long appId)
        {
            return _administratorGrpcService.GetApplicationById(appId);
        }

        [HttpGet]
        public async Task<List<ApplicationDTO>> GetApplicationsFromArchive()
        {
            return _administratorGrpcService.GetApplicationsFromArchive();
        }

        [HttpGet]
        public async Task<List<ApplicationDTO>> GetActiveApplications()
        {
            return _administratorGrpcService.GetActiveApplications();
        }

        [HttpGet]
        public async Task<List<StaffDTO>> GetStaffFromArchive()
        {
            return _administratorGrpcService.GetStaffFromArchive();
        }

        [HttpGet]
        public async Task<List<ResidentDTO>> GetOwnersFromArchive()
        {
            return _administratorGrpcService.GetOwnersFromArchive();
        }

        [HttpDelete]
        public async Task<long> DeleteStaffById(long staffId)
        {
            return _administratorGrpcService.DeleteStaffById(staffId);
        }

        [HttpDelete]
        public async Task<long> DeleteOwnerById(long ownerId)
        {
            return _administratorGrpcService.DeleteOwnerById(ownerId);
        }
    }
}
