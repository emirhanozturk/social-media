using Application.Abstracts.Services;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.AppUser.GetUserProfilePhoto
{
    public class GetUserProfilePhotoQueryHandler : IRequestHandler<GetUserProfilePhotoQueryRequest, GetUserProfilePhotoQueryResponse>
    {
        private readonly IConfiguration _configuration;
        private readonly IUserService _userService;
        private readonly UserManager<Domain.Entities.Identity.AppUser> _userManager;

        public GetUserProfilePhotoQueryHandler(IUserService userService, IConfiguration configuration, UserManager<Domain.Entities.Identity.AppUser> userManager)
        {
            _userService = userService;
            _configuration = configuration;
            _userManager = userManager;
        }

        public async Task<GetUserProfilePhotoQueryResponse> Handle(GetUserProfilePhotoQueryRequest request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.Include(u=>u.ProfilePhotos).FirstOrDefault(u=>u.Id== request.Id);
            if(user == null)
            {
                return new();
            }
            return user.ProfilePhotos.Select(p => new GetUserProfilePhotoQueryResponse
            {
                Id = p.Id,
                Path = $"{_configuration["StorageUrl"]}/{p.Path}",
                FileName = p.FileName
            }).First();
        }
    }
}
