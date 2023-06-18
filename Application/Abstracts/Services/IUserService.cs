using Application.Dtos.User;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Services
{
    public interface IUserService
    {
        Task<CreateUserResponseDto> CreateAsync(CreateUserDto createUserDto);
        Task RefreshTokenUpdate(string refreshToken, AppUser appUser, DateTime accessTokenExpireDate, int addToAccessTokenDate);

        Task<List<GetAllUserDto>> GetAllUsersAsync(int page,int size);
        int TotalCount { get; }

        Task AssignRoleToUserAsync(string userId, string[] roles);
        Task<string[]> GetRolesUser(string userId);
        Task<AppUser> GetAppUserById(string userId);

        Task<AppUser> GetCurrentUser(string username);
    }
}
