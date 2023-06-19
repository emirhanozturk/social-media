namespace Application.Features.Queries.AppUser.GetUserProfilePhoto
{
    public class GetUserProfilePhotoQueryResponse
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}