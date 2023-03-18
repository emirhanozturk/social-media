using Application.Dtos;

namespace Application.Features.Commands.AppUsers.Login
{
    public class LoginUserCommandResponse
    {
    }
    public class LoginUserSuccessCommandResponse: LoginUserCommandResponse
    {
        public Token Token { get; set; }
    }
    public class LoginUserErrorCommandResponse : LoginUserCommandResponse
    {
        public string Message { get; set; }
    }
}