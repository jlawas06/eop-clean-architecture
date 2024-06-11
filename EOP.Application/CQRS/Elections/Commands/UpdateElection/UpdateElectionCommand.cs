using AutoMapper;
using EOP.Application.Dtos;
using EOP.Application.Interfaces;
using EOP.Application.MappingProfiles;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Elections.Commands
{
    public class UpdateElectionCommand : IRequest<ElectionDto>
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }

    internal class UpdateElectionCommandHandler : IRequestHandler<UpdateElectionCommand, ElectionDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UpdateElectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<UpdateElectionCommand, Election>();
                cfg.AddProfile<ElectionProfile>();
            });

            _mapper = config.CreateMapper();
        }

        public async Task<ElectionDto> Handle(UpdateElectionCommand request, CancellationToken cancellationToken)
        {
            var election = await _unitOfWork.GetRepository<Election>().GetByIdAsync(request.Id);

            if (election == null) throw new Exception($"Election with id {request.Id} not found.");
            

            election.Name = request.Name;
            election.Description = request.Description;
            election.StartDate = request.StartDate;
            election.EndDate = request.EndDate;

            await _unitOfWork.CommitAsync();

            return _mapper.Map<ElectionDto>(election);
        }
    }
}
