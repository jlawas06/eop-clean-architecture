using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Candidates.Queries
{
    public class GetCandidatesQuery : IRequest<IEnumerable<CandidateDto>>
    {
    }

    internal class GetCandidatesQueryHandler : IRequestHandler<GetCandidatesQuery, IEnumerable<CandidateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCandidatesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CandidateProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<IEnumerable<CandidateDto>> Handle(GetCandidatesQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _unitOfWork.GetRepository<Candidate>().GetAllAsync();
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }
    }
}
