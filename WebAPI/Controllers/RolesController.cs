using Application.CustomAttributes;
using Application.Enums;
using Application.Features.Commands.Role.CreateRole;
using Application.Features.Commands.Role.DeleteRole;
using Application.Features.Commands.Role.UpdateRole;
using Application.Features.Queries.Role.GetRoleById;
using Application.Features.Queries.Role.GetRoles;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        readonly IMediator _mediator;

        public RolesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [AuthorizeDefinition(ActionType = ActionType.Reading,Definition = "Get Roles",Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromQuery] GetRolesQueryRequest getRolesQueryRequest)
        {
            GetRolesQueryResponse getRolesQueryResponse = await _mediator.Send(getRolesQueryRequest);
            return Ok(getRolesQueryResponse);
        }
        [HttpGet("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Role By Id", Menu = "Roles")]
        public async Task<IActionResult> GetRoles([FromRoute] GetRoleByIdQueryRequest getRoleByIdQueryRequest)
        {
            GetRoleByIdQueryResponse getRoleByIdQueryResponse = await _mediator.Send(getRoleByIdQueryRequest);
            return Ok(getRoleByIdQueryResponse);
        }

        [HttpPost]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Create Role", Menu = "Roles")]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommandRequest createRoleCommandRequest)
        {
           CreateRoleCommandResponse createRoleCommandResponse  =await _mediator.Send(createRoleCommandRequest);
            return Ok(createRoleCommandResponse);
        }
        [HttpDelete("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Deleting, Definition = "Delete Role", Menu = "Roles")]
        public async Task<IActionResult> DeleteRole([FromRoute] DeleteRoleCommandRequest deleteRoleCommandRequest)
        {
         DeleteRoleCommandResponse deleteRoleCommandResponse =await  _mediator.Send(deleteRoleCommandRequest);
            return Ok(deleteRoleCommandResponse);
        }
        [HttpPut("{Id}")]
        [AuthorizeDefinition(ActionType = ActionType.Updating, Definition = "Update Role", Menu = "Roles")]
        public async Task<IActionResult> UpdateRole([FromBody, FromRoute] UpdateRoleCommandRequest updateRoleCommandRequest)
        {
            UpdateRoleCommandResponse updateRoleCommandResponse = await _mediator.Send(updateRoleCommandRequest);
            return Ok(updateRoleCommandResponse);
        }
    }
}
