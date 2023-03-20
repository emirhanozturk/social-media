using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Services.Auth
{
    public interface IInternalAuth
    {
        Task<Dtos.Token> LoginAsync(string usernameOrEmail,string password, int tokenLifetime);
    }
}
