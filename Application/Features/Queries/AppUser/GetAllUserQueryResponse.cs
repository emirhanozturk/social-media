namespace Application.Features.Queries.AppUser
{
    public class GetAllUsersQueryResponse
    {
        public object Users { get; set; }
        public int TotalUsersCount { get; set; }
    }
}