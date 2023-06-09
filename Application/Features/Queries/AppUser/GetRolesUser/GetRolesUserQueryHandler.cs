using Application.Abstracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.AppUser.GetRolesUser
{
    public class GetRolesUserQueryHandler : IRequestHandler<GetRolesUserQueryRequest, GetRolesUserQueryResponse>
    {
        private readonly IUserService _userService;

        public GetRolesUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetRolesUserQueryResponse> Handle(GetRolesUserQueryRequest request, CancellationToken cancellationToken)
        {
            var roles =  await _userService.GetRolesUser(request.UserId);

            return new() { Roles = roles };
        }
    }
}
