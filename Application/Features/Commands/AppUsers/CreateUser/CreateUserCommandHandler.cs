using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.AppUsers.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
           IdentityResult result = await _userManager.CreateAsync( new()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = request.FirstName,
                LastName = request.LastName,
                UserName= request.UserName,
                Email= request.Email
            },request.Password);

            CreateUserCommandResponse createUserCommandResponse = new CreateUserCommandResponse() {IsSuccess = result.Succeeded };

            if(createUserCommandResponse.IsSuccess)
            {
                createUserCommandResponse.Message = "Kullanıcı oluşturma başarılı";
            }
            else
                foreach(var error in result.Errors)
                {
                    createUserCommandResponse.Message = $"Error Code : {error.Code} Description of Error: {error.Description}\n";
                }
            return createUserCommandResponse;

        }
    }
}
