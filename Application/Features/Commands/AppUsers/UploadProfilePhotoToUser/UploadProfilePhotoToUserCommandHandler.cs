using Application.Abstracts.Services;
using Application.Abstracts.Storage;
using Application.Repositories;
using Application.Repositories.ProfilePhotos;
using Domain.Entities;
using Domain.Entities.Identity;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.AppUsers.UploadProfilePhotoToUser
{
    public class UploadProfilePhotoToUserCommandHandler : IRequestHandler<UploadProfilePhotoToUserCommandRequest, UploadProfilePhotoToUserCommandResponse>
    {
        readonly IProfilePhotoWriteRepository  _profilePhotoWriteRepository;
        readonly IStorageService _storageService;
        readonly IUserService _userService;

        public UploadProfilePhotoToUserCommandHandler(IImageWriteRepository imageWriteRepository, IStorageService storageService, IUserService userService, IProfilePhotoWriteRepository profilePhotoWriteRepository)
        {
            _storageService = storageService;
            _userService = userService;
            _profilePhotoWriteRepository = profilePhotoWriteRepository;
        }

        public async Task<UploadProfilePhotoToUserCommandResponse> Handle(UploadProfilePhotoToUserCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string path)> result = await _storageService.UploadAsync("profilephotos", request.FormFiles);

            AppUser appUser = await _userService.GetAppUserById(request.Id);
            if (appUser == null)
            {
                return null;
            }

            await _profilePhotoWriteRepository.AddRangeAsync(result.Select(p => new ProfilePhoto()
            {
                FileName = p.fileName,
                Path = p.path,
                Storage = _storageService.StorageName,
                AppUser = appUser
            }).ToList());

            await _profilePhotoWriteRepository.SaveAsync();
            return new();
        }
    }
}
