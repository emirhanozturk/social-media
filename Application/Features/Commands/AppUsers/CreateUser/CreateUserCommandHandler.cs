using Application.Abstracts.Services;
using Application.Dtos.User;
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
        private readonly IUserService _userService;

        public CreateUserCommandHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            CreateUserResponseDto createUserResponseDto =  await _userService.CreateAsync(new()
            {
               Email= request.Email,
               FirstName= request.FirstName,
               LastName= request.LastName,
               UserName = request.UserName,
               Password= request.Password,
               ConfirmPassword = request.ConfirmPassword
           });


            return new()
            {
                Message = createUserResponseDto.Message,
                IsSuccess= createUserResponseDto.IsSuccess,
            };

        }
    }
}
