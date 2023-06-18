using Domain.Entities.Identity;

namespace Application.Features.Queries.AppUser.GetCurrentUser
{
    public class GetCurrentUserQueryResponse
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}