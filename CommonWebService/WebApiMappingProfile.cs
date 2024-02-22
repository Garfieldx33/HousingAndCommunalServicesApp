using AutoMapper;
using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonWebService.Services;
using Google.Protobuf.WellKnownTypes;

namespace CommonWebService
{
    public class WebApiMappingProfile : Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<ApplicationDTO, Application>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.StatusId)));

            CreateMap<UserGrpc, User>()
            .ForMember(d => d.TypeId, opt => opt.MapFrom(source => System.Enum.GetName(typeof(UserTypeEnum), source.TypeId)))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime()))
            .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToDateTime()));

            CreateMap<User, UserGrpc>()
            .ForMember(d => d.TypeId, opt => opt.MapFrom(source => (int)source.TypeId))
            .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToTimestamp()))
            .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToTimestamp()));

            CreateMap<DateTime, Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));
        }
    }
}
