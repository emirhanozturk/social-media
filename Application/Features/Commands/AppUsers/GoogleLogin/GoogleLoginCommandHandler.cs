using Application.Abstracts.Token;
using Application.Dtos;
using Domain.Entities.Identity;
using Google.Apis.Auth;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace Application.Features.Commands.AppUsers.GoogleLogin
{
    public class GoogleLoginCommandHandler : IRequestHandler<GoogleLoginCommandRequest, GoogleLoginCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly ITokenHandler _tokenHandler;

        public GoogleLoginCommandHandler(UserManager<AppUser> userManager, ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<GoogleLoginCommandResponse> Handle(GoogleLoginCommandRequest request, CancellationToken cancellationToken)
        {
           var settings = new GoogleJsonWebSignature.ValidationSettings()
           {
               Audience = new List<string> { "583294401876-67tnsv795our912o801bff9os738gbib.apps.googleusercontent.com" }
           };

           var payload = await GoogleJsonWebSignature.ValidateAsync(request.IdToken,settings);

           UserLoginInfo userLoginInfo = new UserLoginInfo(request.Provider,payload.Subject,request.Provider);
           AppUser appUser = await _userManager.FindByLoginAsync(userLoginInfo.LoginProvider, userLoginInfo.ProviderKey);

            bool result = appUser != null;
            if(appUser == null)
            {
                appUser = await _userManager.FindByEmailAsync(payload.Email);
                if(appUser == null)
                {
                    appUser = new()
                    {
                        Id = Guid.NewGuid().ToString(),
                        Email = payload.Email,
                        UserName = payload.Email,
                        FirstName=payload.Name,
                        LastName = payload.FamilyName
                    };

                    IdentityResult identityResult  = await _userManager.CreateAsync(appUser);
                    result = identityResult.Succeeded;
                }
            }
            if (result)
            {
              await  _userManager.AddLoginAsync(appUser, userLoginInfo);
            }
            else
            {
                throw new Exception();
            }


            Token token = _tokenHandler.CreateAccessToken();
            return new() { Token = token };
        }
    }
}
