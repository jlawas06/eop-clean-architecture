using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Elections.Commands
{
    public class CreateElectionCommand : IRequest<ElectionDto>
    {
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    internal class CreateElectionCommandHandler : IRequestHandler<CreateElectionCommand, ElectionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CreateElectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<CreateElectionCommand, Election>();
                cfg.AddProfile<ElectionProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<ElectionDto> Handle(CreateElectionCommand request, CancellationToken cancellationToken)
        {
            var election = _mapper.Map<Election>(request);

            await _unitOfWork.GetRepository<Election>().AddAsync(election);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<ElectionDto>(election);
        }
    }
}
