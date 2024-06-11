using EOP.Application.Interfaces;
using EOP.Domain.Entities;
using MediatR;

namespace EOP.Application.CQRS.Candidates.Commands
{
    public class DeleteCandidateCommand : IRequest<string>
    {
        public Guid Id { get; set; }
    }

    internal class DeleteCandidateCommandHandler : IRequestHandler<DeleteCandidateCommand, string>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteCandidateCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<string> Handle(DeleteCandidateCommand request, CancellationToken cancellationToken)
        {
            var candidate = await _unitOfWork.GetRepository<Candidate>().GetByIdAsync(request.Id);

            if (candidate == null) throw new Exception("Candidate not found");

            _unitOfWork.GetRepository<Candidate>().Remove(candidate);
            await _unitOfWork.CommitAsync();

            return "Candidate deleted successfully";
        }
    }
}
