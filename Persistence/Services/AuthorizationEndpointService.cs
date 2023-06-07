using Application.Abstracts.Services;
using Application.Abstracts.Services.Configurations;
using Application.Repositories;
using Domain.Entities;
using Domain.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class AuthorizationEndpointService : IAuthorizationEndpointService
    {
        readonly IApplicationService _applicationService;
        readonly IEndpointReadRepository _endpointReadRepository;
        readonly IEndpointWriteRepository _endpointWriteRepository;
        readonly IMenuReadRepository _menuReadRepository;
        readonly IMenuWriteRepository _menuWriteRepository;
        readonly RoleManager<AppRole> _roleManager;

        public AuthorizationEndpointService(IApplicationService applicationService, IEndpointReadRepository endpointReadRepository, IEndpointWriteRepository endpointWriteRepository, IMenuReadRepository menuReadRepository, IMenuWriteRepository menuWriteRepository, RoleManager<AppRole> roleManager)
        {
            _applicationService = applicationService;
            _endpointReadRepository = endpointReadRepository;
            _endpointWriteRepository = endpointWriteRepository;
            _menuReadRepository = menuReadRepository;
            _menuWriteRepository = menuWriteRepository;
            _roleManager = roleManager;
        }

        public async Task AssignRoleEndpointAsync(string[] roles,string menu, string code,Type type)
        {
            Menu _menu = await _menuReadRepository.GetSingleAsync(m=>m.Name == menu);
            if(_menu== null)
            {
                _menu = new()
                {
                    Id = Guid.NewGuid(),
                    Name = menu
                };
                await   _menuWriteRepository.AddAsync(_menu);
                await _endpointWriteRepository.SaveAsync();
            }


            Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.Menu).Include(e => e.AppRoles).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if (endpoint == null)
            {
               var action = _applicationService.GetAuthorizeDefinitionEndpoints(type).FirstOrDefault(m=>m.Name==menu)
                    ?.Actions.FirstOrDefault(e=>e.Code == code);

                endpoint = new()
                {
                    Code = action.Code,
                    ActionType = action.ActionType,
                    HttpType = action.HttpType,
                    Definition = action.Definition,
                    Id = Guid.NewGuid(),
                    Menu = _menu,
                };

               await _endpointWriteRepository.AddAsync(endpoint);
               await _endpointWriteRepository.SaveAsync();
            }

            foreach (var role in endpoint.AppRoles)
            {
                endpoint.AppRoles.Remove(role);
            }


            var appRoles = await _roleManager.Roles.Where(r => roles.Contains(r.Name)).ToListAsync();

            foreach (var role in appRoles)
            {
                endpoint.AppRoles.Add(role);
            }
            await _endpointWriteRepository.SaveAsync();


        }

        public async Task<List<string>> GetRolesToEndpointAsync(string code, string menu)
        {
           Endpoint? endpoint = await _endpointReadRepository.Table.Include(e => e.AppRoles).Include(e => e.Menu).FirstOrDefaultAsync(e => e.Code == code && e.Menu.Name == menu);
            if(endpoint != null)
            {
                return endpoint.AppRoles.Select(r => r.Name).ToList();
            }
            return null;
            
        }
    }
}
