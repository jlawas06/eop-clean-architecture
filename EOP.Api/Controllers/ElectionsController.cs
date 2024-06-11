using EOP.Application.CQRS.Elections.Commands;
using EOP.Application.CQRS.Elections.Queries;
using EOP.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EOP.Api.Controllers
{
    [Authorize]
    public class ElectionsController : ApiBaseController
    {
        public ElectionsController(IMediator mediator) : base(mediator)
        {
        }

        [HttpGet(Name = "GetElections")]
        public async Task<IEnumerable<ElectionDto>> GetElections() => await _mediator.Send(new GetElectionsQuery());

        [HttpGet("{id}",Name = "GetElectionById")]
        public async Task<ActionResult<ElectionDto>> GetElectionById(Guid id) => await _mediator.Send(new GetElectionByIdQuery { Id = id });

        [HttpPost(Name = "CreateElection")]
        public async Task<ActionResult<ElectionDto>> CreateElection([FromBody] CreateElectionCommand command) => await _mediator.Send(command);

        [HttpPut("{id}", Name = "UpdateElection")]
        public async Task<ActionResult<ElectionDto>> UpdateElection(Guid id, [FromBody] UpdateElectionCommand command)
        {
            command.Id = id;
            return await _mediator.Send(command);
        }

        [HttpDelete("{id}", Name = "DeleteElection")]
        public async Task<ActionResult<string>> DeleteElection(Guid id) => await _mediator.Send(new DeleteElectionCommand { Id = id });
    }
}
