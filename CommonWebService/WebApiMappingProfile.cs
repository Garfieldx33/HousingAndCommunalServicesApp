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
            .ForMember(d => d.TypeId, opt => opt.MapFrom(source => System.Enum.GetName(typeof(UserTypeEnum), source.TypeId)));

            CreateMap<User, UserGrpc>()
            .ForMember(d => d.TypeId, opt => opt.MapFrom(source => (int)source.TypeId));

            CreateMap<UserDTO, UserGrpc>()
            .ForMember(d => d.TypeId, opt => opt.MapFrom(source => source.UserTypeId))
            .ForMember(d => d.SecondName, opt => opt.MapFrom(source => source.Surname));

            CreateMap<UpdateAppDto, UpdateAppRequest>().ReverseMap()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.Status)));

            CreateMap<DateTime, Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));
        }
    }
}
