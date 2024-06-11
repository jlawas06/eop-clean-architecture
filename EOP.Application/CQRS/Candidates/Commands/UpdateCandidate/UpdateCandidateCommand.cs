using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Candidates.Commands
{
    public class UpdateCandidateCommand : IRequest<CandidateDto>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
    }

    internal class UpdateCandidateCommandHandler : IRequestHandler<UpdateCandidateCommand, CandidateDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateCandidateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<CandidateProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<CandidateDto> Handle(UpdateCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.GetRepository<Candidate>().GetByIdAsync(request.Id);

            if (candidate == null) throw new Exception($"Candidate with id {request.Id} not found");

            candidate.Name = request.Name;
            await _unitOfWork.CommitAsync();
            return _mapper.Map<CandidateDto>(candidate);
        }
    }
}
