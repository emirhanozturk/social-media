using Application.CustomAttributes;
using Application.Enums;
using Application.Features.Commands.AppUsers.CreateUser;
using Application.Features.Commands.AppUsers.GoogleLogin;
using Application.Features.Commands.AppUsers.Login;
using Application.Features.Queries.AppUser;
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

       


    }
}
