using Application.Features.Commands.AuthorizationEndpoint.AssignRoleEndpoint;
using Application.Features.Queries.AuthorizationEndpoint.GetRolesToEndpoint;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorizationEndpointsController : ControllerBase
    {

        private readonly IMediator _mediator;

        public AuthorizationEndpointsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> GetRolesToEndpoints(GetRolesToEndpointQueryRequest getRolesToEndpointQueryRequest)
        {
          GetRolesToEndpointQueryResponse getRolesToEndpointQueryResponse = await  _mediator.Send(getRolesToEndpointQueryRequest);
            return Ok(getRolesToEndpointQueryResponse);
        }

        [HttpPost]
        public async Task<IActionResult> AssignRoleEndpoint(AssignRoleEndpointCommandRequest assignRoleEndpointCommandRequest)
        {
            assignRoleEndpointCommandRequest.Type = typeof(Program);
           AssignRoleEndpointCommandResponse assignRoleEndpointCommandResponse = await _mediator.Send(assignRoleEndpointCommandRequest);
            return Ok(assignRoleEndpointCommandResponse);
        }
    }
}
