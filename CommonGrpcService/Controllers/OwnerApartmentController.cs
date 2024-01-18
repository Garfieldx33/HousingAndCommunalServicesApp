using CommonGrpcService.Services;
using CommonLib.Services.Models;
using Microsoft.AspNetCore.Mvc;

namespace CommonGrpcService.Controllers
{

    [ApiController]
    [Route("[controller]/[action]")]
    public class OwnerApartmentController : ControllerBase
    {
        OwnerApartmentGrpcService _ownerapartmentService;
        public OwnerApartmentController(OwnerApartmentGrpcService ownerApartmentGrpcService)
        {
            _ownerapartmentService = ownerApartmentGrpcService;
        }

        [HttpGet]
        public async Task<InvoiceDataResponce> GetInvoce(int apartmentId, DateTime period)
        {
            return _ownerapartmentService.GetInvoce(apartmentId, period);
        }

        [HttpPost]
        public async Task<long> NewOwner([FromBody] NewOwnerRequest request)
        {
            return _ownerapartmentService.NewOwner(request);
        }

        [HttpGet]
        public async Task<OwnerDataResponce> GetOwnerDataByInn(int inn)
        {
            return _ownerapartmentService.GetOwnerDataByInn(inn);
        }

        [HttpGet]
        public async Task<OwnerDataResponce> GetDebtOwnerByInn(int inn)
        {
            return _ownerapartmentService.GetDebtOwnerByInn(inn);
        }

        [HttpPut]
        public async Task<long> UpdateOwnerData(long inn)
        {
            return _ownerapartmentService.UpdateOwnerDataByInn(inn);
        }

        [HttpDelete]
        public async Task<long> DeleteOwner(long inn)
        {
            return _ownerapartmentService.DeleteOwner(inn);
        }
    }
}
