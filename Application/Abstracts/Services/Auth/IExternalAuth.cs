using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Services.Auth
{
    public interface IExternalAuth
    {
        Task<Dtos.Token> GoogleLoginAsync(string idToken,int tokenLifeTime);
    }
}
