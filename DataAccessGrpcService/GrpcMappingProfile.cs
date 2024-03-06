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
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.Status)))
            .ForMember(d => d.ApplicantId, opt => opt.MapFrom(source => source.ApplicantId))
            .ForMember(d => d.Subject, opt => opt.MapFrom(source => source.Subject))
            .ForMember(d => d.ApplicationTypeId, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppTypeEnum), source.ApplicationTypeId)))
            .ForMember(d => d.Description, opt => opt.MapFrom(source => source.Description))
            .ForMember(d => d.DateCreate, opt => opt.MapFrom(source => source.DateCreate.ToDateTime()));

            CreateMap<ApplicationGrpc, Application>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.Status)));

            CreateMap<Application, ApplicationGrpc>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => (int)source.Status))
                .ForMember(d => d.DateCreate, opt => opt.MapFrom(source => Timestamp.FromDateTime(DateTime.SpecifyKind(source.DateCreate, DateTimeKind.Utc))));

            CreateMap<Department, DepartmentGrpc>()
                .ForMember(d => d.DepartmentId, opt => opt.MapFrom(source => source.Id))
                .ForMember(d => d.DepartmentName, opt => opt.MapFrom(source => source.Name));

            CreateMap<UserGrpc, User>()
                .ForMember(d => d.TypeId, opt => opt.MapFrom(source => System.Enum.GetName(typeof(UserTypeEnum), source.TypeId)))
                .ForMember(d => d.SecondName, opt => opt.MapFrom(source => source.SecondName))
                .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime()))
                .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToDateTime()));

            CreateMap<User, UserGrpc>()
                .ForMember(d => d.TypeId, opt => opt.MapFrom(source => (int)source.TypeId));

            CreateMap<UpdateAppDTO, UpdateAppRequest>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => source.Status))
                .ForMember(d => d.DepartmentId, opt => opt.MapFrom(source => source.DepartmentId))
                .ForMember(d => d.DateClose, opt => opt.MapFrom(source => Timestamp.FromDateTime(DateTime.SpecifyKind(source.DateClose, DateTimeKind.Utc))))
                .ForMember(d => d.DateConfirm, opt => opt.MapFrom(source => Timestamp.FromDateTime(DateTime.SpecifyKind(source.DateConfirm, DateTimeKind.Utc))));

            CreateMap<UpdateAppRequest, UpdateAppDTO>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => source.Status))
                .ForMember(d => d.DateClose, opt => opt.MapFrom(source => source.DateClose.ToDateTime()))
                .ForMember(d => d.DateConfirm, opt => opt.MapFrom(source => source.DateConfirm.ToDateTime()));

            CreateMap<DateTime, Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));

        }
    }
}
