using AutoMapper;
using CommonLib.DTO;
using CommonLib.Entities;

namespace DataAccessWebService
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<ApplicationDTO,Application>().ReverseMap();
        }
    }
}
