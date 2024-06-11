using EOP.Application.CQRS.Candidates.Commands;
using EOP.Application.CQRS.Candidates.Queries;
using EOP.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EOP.Api.Controllers
{
    [Authorize]
    public class CandidatesController : ApiBaseController
    {
        public CandidatesController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(Name = "GetCandidates")]
        public async Task<IEnumerable<CandidateDto>> GetCandidates() => await _mediator.Send(new GetCandidatesQuery());

        [HttpGet("{id}", Name = "GetCandidateById")]
        public async Task<ActionResult<CandidateDto>> GetCandidateById(Guid id) => await _mediator.Send(new GetCandidateByIdQuery { Id = id });

        [HttpGet("get-candidates-by-election",Name = "GetCandidatesByElectionId")]
        public async Task<IEnumerable<CandidateDto>> GetCandidatesByElectionId(Guid electionId) => await _mediator.Send(new GetCandidatesByElectionIdQuery { ElectionId = electionId });

        [HttpPost(Name = "CreateCandidate")]
        public async Task<ActionResult<CandidateDto>> CreateCandidate([FromBody] CreateCandidateCommand command) => await _mediator.Send(command);

        [HttpPut("{id}", Name = "UpdateCandidate")]
        public async Task<ActionResult<CandidateDto>> UpdateCandidate(Guid id, [FromBody] UpdateCandidateCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}", Name = "DeleteCandidate")]
        public async Task<ActionResult<string>> DeleteCandidate(Guid id) => await _mediator.Send(new DeleteCandidateCommand { Id = id });

    }
}
