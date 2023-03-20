using Application.Dtos;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUsers.GoogleLogin
{
    public class GoogleLoginCommandResponse
    {
        public Token Token { get; set; }
    }
}
