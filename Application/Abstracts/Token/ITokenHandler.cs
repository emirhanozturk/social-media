using Application.Dtos;
using Domain.Entities.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Token
{
    public interface ITokenHandler
    {
        Dtos.Token CreateAccessToken(int tokenLifeTime,AppUser appUser);
        string CreateRefreshToken();

    }
}
