using AutoMapper;
using CommonLib.DTO;
using CommonLib.Entities;
using CommonLib.Enums;

namespace CommonLib.Helpers
{
    public class WebApiMappingProfile : Profile
    {
        public WebApiMappingProfile()
        {
            CreateMap<ApplicationDTO, Application>()
                .ForMember(d => d.Status, opt => opt.MapFrom(source => Enum.GetName(typeof(AppStatusEnum), source.StatusId))); 
        }
    }
}
