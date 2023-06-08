using Application.Abstracts.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.AppUser
{
    public class GetAllUserQueryHandler : IRequestHandler<GetAllUsersQueryRequest, GetAllUsersQueryResponse>
    {

        readonly IUserService _userService;

        public GetAllUserQueryHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task<GetAllUsersQueryResponse> Handle(GetAllUsersQueryRequest request, CancellationToken cancellationToken)
        {
          var users = await _userService.GetAllUsersAsync(request.Page, request.Size);
            return new()
            {
                Users = users,
                TotalUsersCount = _userService.TotalCount
            };
        }
    }
}
