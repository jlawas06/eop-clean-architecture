using AutoMapper;
using EOP.Application.Dtos;
using EOP.Domain.Entities;

namespace EOP.Application.MappingProfiles
{
    internal class CandidateProfile : Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateDto>()
                 .ForMember(dest => dest.Votes, opt => opt.MapFrom(src => src.Votes.Count));
        }
    }
}
