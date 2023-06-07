using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Services
{
    public interface IAuthorizationEndpointService
    {
        public Task AssignRoleEndpointAsync(string[] roles, string Menu, string code, Type type);
        public Task<List<string>> GetRolesToEndpointAsync(string code,string menu);
    }
}
