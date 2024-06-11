using AutoMapper;
using EOP.Application.Dtos;
using EOP.Domain.Entities;

namespace EOP.Application.MappingProfiles
{
    internal class ElectionProfile : Profile
    {
        public ElectionProfile()
        {
            CreateMap<Election, ElectionDto>();
        }
    }
}
