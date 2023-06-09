using Application.CustomAttributes;
using Application.Enums;
using Application.Features.Commands.AppUsers.AssignRoleToUser;
using Application.Features.Commands.AppUsers.CreateUser;
using Application.Features.Commands.AppUsers.GoogleLogin;
using Application.Features.Commands.AppUsers.Login;
using Application.Features.Queries.AppUser.GetAllUsers;
using Application.Features.Queries.AppUser.GetRolesUser;
using Application.Features.Queries.Posts.GetAllPost;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        readonly IMediator _mediator;

        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser(CreateUserCommandRequest createUserCommandRequest)
        {
            CreateUserCommandResponse createUserCommandResponse = await _mediator.Send(createUserCommandRequest); 
            return Ok(createUserCommandResponse);
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes ="Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading,Definition = "Get All Users", Menu = "Users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] GetAllUsersQueryRequest getAllUserQueryRequest)
        {
            GetAllUsersQueryResponse getAllUsersQueryResponse = await _mediator.Send(getAllUserQueryRequest); 
            return Ok(getAllUsersQueryResponse);
        }

        [HttpGet("get-roles-user/{userId}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Reading, Definition = "Get Roles To User", Menu = "Users")]
        public async Task<IActionResult> GetRolesToUser([FromRoute] GetRolesUserQueryRequest getRolesUserQueryRequest)
        {
            GetRolesUserQueryResponse getRolesUserQueryResponse = await _mediator.Send(getRolesUserQueryRequest);
            return Ok(getRolesUserQueryResponse);
        }

        [HttpPost("assign-role-to-user")]
        [Authorize(AuthenticationSchemes = "Admin")]
        [AuthorizeDefinition(ActionType = ActionType.Writing, Definition = "Assign Role to User", Menu = "Users")]
        public async Task<IActionResult> AssignRoleToUser(AssignRoleToUserCommandRequest assignRoleToUserCommandRequest)
        {
            AssignRoleToUserCommandResponse assignRoleToUserCommandResponse = await _mediator.Send(assignRoleToUserCommandRequest);
            return Ok(assignRoleToUserCommandResponse);
        }

       


    }
}
