using AutoMapper;
using CommonLib.DTO;
using CommonLib.Enums;
using Google.Protobuf.WellKnownTypes;

namespace NewAppPrepareService
{
    public class NewAppPrepareProfile : Profile
    {
        public NewAppPrepareProfile()
        {
            CreateMap<ApplicationDTO, ApplicationDtoGrpc>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => source.StatusId))
            .ForMember(d => d.DateCreate, opt => opt.MapFrom(source => Timestamp.FromDateTime(DateTime.SpecifyKind(source.DateCreate, DateTimeKind.Utc))));
        }
    }
}
