using Application.Abstracts.Services;
using Application.Dtos.User;
using Application.Features.Commands.AppUsers.CreateUser;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;

        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto)
        {
            IdentityResult result = await _userManager.CreateAsync(new()
            {
                Id = Guid.NewGuid().ToString(),
                FirstName = createUserDto.FirstName,
                LastName = createUserDto.LastName,
                UserName = createUserDto.UserName,
                Email = createUserDto.Email
            }, createUserDto.Password);

            CreateUserResponseDto createUserResponseDto = new CreateUserResponseDto() { IsSuccess = result.Succeeded };

            if (createUserResponseDto.IsSuccess)
            {
                createUserResponseDto.Message = "Kullanıcı oluşturma başarılı";
            }
            else
                foreach (var error in result.Errors)
                {
                    createUserResponseDto.Message = $"Error Code : {error.Code} Description of Error: {error.Description}\n";
                }
            return createUserResponseDto;
        }

        public async Task<List<GetAllUserDto>> GetAllUsersAsync(int page, int size)
        {
            var users =  await _userManager.Users.Skip(page*size).Take(size).ToListAsync();

            return users.Select(user => new GetAllUserDto
            {
               Id = user.Id,
               Email = user.Email,
               FirstName = user.FirstName,
               LastName = user.LastName,
               TwoFactorEnabled= user.TwoFactorEnabled,
               Username = user.UserName,
            }).ToList();
        }

        public int TotalCount => _userManager.Users.Count();


        public async Task RefreshTokenUpdate(string refreshToken, AppUser appUser, DateTime accessTokenExpireDate,int addToAccessTokenDate)
        {
            if (appUser != null)
            {
                appUser.RefreshToken = refreshToken;
                appUser.RefreshTokenEndDate = accessTokenExpireDate.AddSeconds(addToAccessTokenDate);
                await _userManager.UpdateAsync(appUser);
            }
            else
                throw new Exception("UserNotFound");
        }

        public async Task AssignRoleToUserAsync(string userId, string[] roles)
        {
           AppUser appUser = await _userManager.FindByIdAsync(userId);
            if(appUser != null)
            {
              var appUserRoles = await  _userManager.GetRolesAsync(appUser);
              await  _userManager.RemoveFromRolesAsync(appUser, appUserRoles);

                await _userManager.AddToRolesAsync(appUser, roles);
            }
        }

        public async Task<string[]> GetRolesUser(string userId)
        {
            AppUser appUser = await _userManager.FindByIdAsync(userId);
            if(appUser != null)
            {
               var roles = await _userManager.GetRolesAsync(appUser);
               return roles.ToArray();
            }

            return new string[] { };
        }
    }
}
