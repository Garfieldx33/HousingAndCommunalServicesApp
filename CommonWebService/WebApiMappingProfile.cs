﻿using AutoMapper;
using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;
using CommonWebService.Services;
using Google.Protobuf.WellKnownTypes;
using System.Globalization;

namespace CommonWebService;

public class WebApiMappingProfile : Profile
{
    public WebApiMappingProfile()
    {
        CreateMap<ApplicationDTO, Application>()
            .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.StatusId)));

        CreateMap<UserGrpc, User>()
           .ForMember(d => d.TypeId, opt => opt.MapFrom(source => System.Enum.GetName(typeof(UserTypeEnum), source.TypeId)))
           .ForMember(d => d.SecondName, opt => opt.MapFrom(source => source.SecondName))
           .ForMember(dest => dest.DateOfBirth, opt => opt.MapFrom(src => src.DateOfBirth.ToDateTime()))
           .ForMember(dest => dest.RegistrationDate, opt => opt.MapFrom(src => src.RegistrationDate.ToDateTime()));

        CreateMap<User, UserGrpc>()
        .ForMember(d => d.TypeId, opt => opt.MapFrom(source => (int)source.TypeId));

        CreateMap<UserDTO, UserGrpc>()
        .ForMember(d => d.TypeId, opt => opt.MapFrom(source => source.UserTypeId))
        .ForMember(d => d.SecondName, opt => opt.MapFrom(source => source.Surname));

        CreateMap<UpdateAppDTO, UpdateAppRequest>().ReverseMap()
            .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.Status)));

        CreateMap<DateTime, Timestamp>().ConvertUsing(x => Timestamp.FromDateTime(DateTime.SpecifyKind(x, DateTimeKind.Utc)));
        CreateMap<Timestamp, DateTime>().ConvertUsing(x => x.ToDateTime());

        CreateMap<ApplicationGrpc, Application>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => System.Enum.GetName(typeof(AppStatusEnum), source.Status)))
                .ForMember(dest => dest.DateCreate, dest => dest.MapFrom(src => src.DateCreate.ToDateTime()))
                .ForMember(dest => dest.DateClose, dest => dest.MapFrom(src => src.DateClose.ToDateTime()))
                .ForMember(dest => dest.DateConfirm, dest => dest.MapFrom(src => src.DateConfirm.ToDateTime()));

        CreateMap<Application, ApplicationGrpc>()
            .ForMember(d => d.Status, opt => opt.MapFrom(source => (int)source.Status))
            .ForMember(d => d.DateCreate, opt => opt.MapFrom(source => Timestamp.FromDateTime(DateTime.SpecifyKind(source.DateCreate, DateTimeKind.Utc))));

        CreateMap<EmployeeInfoGrpc, EmployeeInfo>().ReverseMap()
            .ForMember(d => d.EmployeeUserId, opt => opt.MapFrom(source => source.EmployeeUserId))
            .ForMember(d => d.DepartmentId, opt => opt.MapFrom(source => source.DepartmentId))
            .ForMember(d => d.Position, opt => opt.MapFrom(source => source.Position));
    }
}
