using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Candidates.Queries
{
    public class GetCandidatesByElectionIdQuery : IRequest<IEnumerable<CandidateDto>>
    {
        public Guid ElectionId { get; set; }
    }

    internal class GetCandidatesByElectionIdQueryHandler : IRequestHandler<GetCandidatesByElectionIdQuery, IEnumerable<CandidateDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCandidatesByElectionIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CandidateProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<IEnumerable<CandidateDto>> Handle(GetCandidatesByElectionIdQuery request, CancellationToken cancellationToken)
        {
            var candidates = await _unitOfWork.GetRepository<Candidate>().FindAsync(c => c.ElectionId == request.ElectionId);
            return _mapper.Map<IEnumerable<CandidateDto>>(candidates);
        }
    }
}
