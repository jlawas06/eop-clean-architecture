using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Candidates.Commands
{
    public class CreateCandidateCommand : IRequest<CandidateDto>
    {
        public required string Name { get; set; }
        public Guid ElectionId { get; set; }
    }

    internal class CreateCandidateCommandHandler : IRequestHandler<CreateCandidateCommand, CandidateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateCandidateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateCandidateCommand, Candidate>();
                cfg.AddProfile<CandidateProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<CandidateDto> Handle(CreateCandidateCommand request, CancellationToken cancellationToken)
        {
            var election = await _unitOfWork.GetRepository<Election>().GetByIdAsync(request.ElectionId);
            if (election == null)  throw new Exception("Election not found");

            var candidate = _mapper.Map<Candidate>(request);
            await _unitOfWork.GetRepository<Candidate>().AddAsync(candidate);
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CandidateDto>(candidate);
        }
    }
}
