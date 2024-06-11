using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Elections.Queries
{
    public class GetElectionByIdQuery : IRequest<ElectionDto>
    {
        public Guid Id { get; set; }
    }

    internal class GetElectionByIdQueryHandler : IRequestHandler<GetElectionByIdQuery, ElectionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public GetElectionByIdQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<ElectionProfile>();
                cfg.AddProfile<CandidateProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<ElectionDto> Handle(GetElectionByIdQuery request, CancellationToken cancellationToken)
        {
            var election = await _unitOfWork.GetRepository<Election>().SingleOrDefaultAsync(x => x.Id == request.Id,"Candidates");
            return _mapper.Map<ElectionDto>(election);
        }
    }
}
