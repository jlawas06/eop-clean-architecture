using EOP.Application.Interfaces;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Elections.Commands
{
    public class DeleteElectionCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteElectionCommandHandler : IRequestHandler<DeleteElectionCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteElectionCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteElectionCommand request, CancellationToken cancellationToken)
        {
            var election = await _unitOfWork.GetRepository<Election>().GetByIdAsync(request.Id);

            if (election == null) throw new Exception($"Election with id {request.Id} not found.");

            _unitOfWork.GetRepository<Election>().Remove(election);
            await _unitOfWork.CommitAsync();

            return $"Election with id {request.Id} deleted.";
        }
    }
}
