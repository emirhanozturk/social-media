using MediatR;
using Microsoft.AspNetCore.Http;

namespace Application.Features.Commands.AppUsers.UploadProfilePhotoToUser
{
    public class UploadProfilePhotoToUserCommandRequest : IRequest<UploadProfilePhotoToUserCommandResponse>
    {
        public string Id { get; set; }
        public IFormFileCollection? FormFiles { get; set; }
    }
}