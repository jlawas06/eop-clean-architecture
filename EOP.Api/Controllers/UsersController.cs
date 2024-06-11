using EOP.Application.CQRS.Users.Queries.Login;
using EOP.Application.Dtos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EOP.Api.Controllers
{
    public class UsersController : ApiBaseController
    {
        public UsersController(IMediator mediator) : base(mediator)
        {
        }

        [AllowAnonymous]
        [HttpPost("login", Name = "Login")]
        public async Task<ActionResult<LoginResponseDto>> Login([FromBody] LoginQuery model)
        {
            return await _mediator.Send(model);
        }
    }
}
