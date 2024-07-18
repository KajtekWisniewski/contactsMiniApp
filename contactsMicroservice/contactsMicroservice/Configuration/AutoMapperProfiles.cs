using AutoMapper;
using ContactsMicroservice.DTOs;
using ContactsMicroservice.Entities;

namespace ContactsMicroservice.Configuration
{
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles()
        {
            //CreateMap<Contact, ContactDto>();
            CreateMap<Contact, ContactMinimalDto>();
            CreateMap<UpsertContactDto, Contact>();
            CreateMap<UpsertContactMinDto, Contact>();
            CreateMap<Contact, ContactDto>()
                .ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category.Name))
                .ForMember(dest => dest.Subcategory, opt => opt.MapFrom(src => src.Subcategory.Name));
        }
    }
}
