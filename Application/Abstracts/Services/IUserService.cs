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
    }
}
