using AutoMapper;
using ContactsMicroservice.DTOs;
using ContactsMicroservice.Entities;

namespace ContactsMicroservice.Configuration
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
