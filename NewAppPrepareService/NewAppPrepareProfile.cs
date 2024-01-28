using AutoMapper;
using CommonLib.DTO;
using CommonLib.Enums;

namespace NewAppPrepareService
{
    public class NewAppPrepareProfile : Profile
    {
        public NewAppPrepareProfile()
        {
            CreateMap<ApplicationDTO, ApplicationDtoGrpc>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.StatusId)));
        }
    }
}
