using AutoMapper;
using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using Google.Protobuf.WellKnownTypes;

namespace DataAccessGrpcService
{
    public class GrpcMappingProfile : Profile
    {
        public GrpcMappingProfile()
        {
            CreateMap<ApplicationDtoGrpc, Application>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.Status)));

            CreateMap<ApplicationGrpc, Application>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.Status)));

            CreateMap<Application, ApplicationGrpc>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => (int)source.Status))
                .ForMember(d => d.DateCreate, opt => opt.MapFrom(source => Timestamp.FromDateTime(DateTime.SpecifyKind(source.DateCreate, DateTimeKind.Utc))));



            CreateMap<DateTime, Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));

        }
    }
}
