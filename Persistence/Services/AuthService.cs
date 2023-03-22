using Application.Abstracts.Services;
using Application.Abstracts.Token;
using Application.Dtos;
using Application.Exceptions;
using Application.Features.Commands.AppUsers.Login;
using Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly IUserService _userService;

        public AuthService(IConfiguration configuration, UserManager<AppUser> userManager, ITokenHandler tokenHandler, SignInManager<AppUser> signInManager, IUserService userService)
        {
            _configuration = configuration;
            _userManager = userManager;
            _tokenHandler = tokenHandler;
            _signInManager = signInManager;
            _userService = userService;
        }

        public async Task<Token> GoogleLoginAsync(string idToken, int tokenLifeTime)
        {
            var settings = new GoogleJsonWebSignature.ValidationSettings()
            {
                Audience = new List<string> { _configuration["ExternalLoginSettings:Google:Client_ID"] }
            };
            var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);

            UserLoginInfo userLoginInfo = new UserLoginInfo("GOOGLE", payload.Subject, "GOOGLE");
            AppUser appUser = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            bool result = appUser != null;
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(payload.Email);
                if (appUser == null)
                {
                    appUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        FirstName = payload.Name,
                        LastName = payload.FamilyName
                    };

                    IdentityResult identityResult = await _userManager.CreateAsync(appUser);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
                await _userManager.AddLoginAsync(appUser, userLoginInfo);
                Token token = _tokenHandler.CreateAccessToken(tokenLifeTime);
                await _userService.RefreshTokenUpdate(token.RefreshToken, appUser,token.Expiration,10);
                return token;
            }
            else
                throw new Exception();


            
        }

        public async Task<Token> LoginAsync(string usernameOrEmail, string password,int tokenLifetime)
        {
            AppUser appUser = await _userManager.FindByNameAsync(usernameOrEmail);
            if (appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(usernameOrEmail);
            }

            if (appUser == null)
            {
                throw new Exception("Kullanıcı bulunamadı");
            }

            SignInResult result = await _signInManager.CheckPasswordSignInAsync(appUser, password, false);
            if (result.Succeeded)
            {
                Token token = _tokenHandler.CreateAccessToken(tokenLifetime);
                await _userService.RefreshTokenUpdate(token.RefreshToken, appUser, token.Expiration, 10);
                return token;
            }
            else
                throw new AuthenticationException();
        }

        public async Task<Token> RefreshTokenLoginAsync(string refreshToken)
        {
         AppUser? appUser =  await _userManager.Users.FirstOrDefaultAsync(u => u.RefreshToken == refreshToken);
            if (appUser != null && appUser?.RefreshTokenEndDate > DateTime.UtcNow)
            {
                Token token = _tokenHandler.CreateAccessToken(20);
                await _userService.RefreshTokenUpdate(token.RefreshToken, appUser, token.Expiration, 15);
                return token;
            }
            else
                throw new Exception("User not found");

        }
    }
}
