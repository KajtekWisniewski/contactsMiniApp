using AutoMapper;
using AuthService.DTOs;
using AuthService.Entities;

namespace AuthService.Configuration
{
    public class AutoMapperProfiles : Profile {
        public AutoMapperProfiles()
        {
            CreateMap<Contact, ContactDto>();
            CreateMap<Contact, ContactMinimalDto>();
            CreateMap<UpsertContactDto, Contact>();
    }
    }
}
