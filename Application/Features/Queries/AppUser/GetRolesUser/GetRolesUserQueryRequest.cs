using MediatR;

namespace Application.Features.Queries.AppUser.GetRolesUser
{
    public class GetRolesUserQueryRequest : IRequest<GetRolesUserQueryResponse>
    {
        public string UserId { get; set; }
    }
}