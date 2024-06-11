using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Candidates.Queries
{
    public class GetCandidateByIdQuery : IRequest<CandidateDto>
    {
        public Guid Id { get; set; }
    }

    internal class GetCandidateByIdQueryHandler : IRequestHandler<GetCandidateByIdQuery, CandidateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetCandidateByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CandidateProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<CandidateDto> Handle(GetCandidateByIdQuery request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.GetRepository<Candidate>().GetByIdAsync(request.Id);
            return _mapper.Map<CandidateDto>(candidate);
        }
    }
}
