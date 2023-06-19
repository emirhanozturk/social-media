using MediatR;

namespace Application.Features.Queries.AppUser.GetUserProfilePhoto
{
    public class GetUserProfilePhotoQueryRequest : IRequest<GetUserProfilePhotoQueryResponse>
    {
        public string Id { get; set; }
    }
}