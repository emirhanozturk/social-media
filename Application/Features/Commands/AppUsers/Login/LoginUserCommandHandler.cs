using Application.Abstracts.Token;
using Application.Dtos;
using Application.Exceptions;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUsers.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;


        public LoginUserCommandHandler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            AppUser user = await _userManager.FindByNameAsync(request.UsernameOrEmail);
            if(user == null)
            {
                user = await _userManager.FindByEmailAsync(request.UsernameOrEmail); 
            }

            if(user == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user,request.Password,false);
            if(result.Succeeded)
            {
              Token token =  _tokenHandler.CreateAccessToken();
                return new LoginUserSuccessCommandResponse() { Token = token };
            }

            throw new AuthenticationException();
        }
    }
}
