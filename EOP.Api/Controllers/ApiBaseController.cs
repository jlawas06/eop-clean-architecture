using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EOP.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class ApiBaseController : ControllerBase
    {
        protected readonly IMediator _mediator;
        public ApiBaseController(IMediator mediator) => _mediator = mediator;
    }
}
