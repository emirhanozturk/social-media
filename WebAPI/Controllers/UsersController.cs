using Application.Features.Commands.AppUsers.CreateUser;
using Application.Features.Commands.AppUsers.GoogleLogin;
using Application.Features.Commands.AppUsers.Login;
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

        [HttpPost("[action]")]
        public async Task<IActionResult> Login(LoginUserCommandRequest loginUserCommandRequest)
        {
           LoginUserCommandResponse loginUserCommandResponse = await _mediator.Send(loginUserCommandRequest);
            return Ok(loginUserCommandResponse);
        }

        [HttpPost("login-with-google")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginCommandRequest googleLoginCommandRequest) 
        { 
            GoogleLoginCommandResponse googleLoginCommandResponse =  await _mediator.Send(googleLoginCommandRequest);
            return Ok(googleLoginCommandResponse);
        }



    }
}
