using Application.Abstracts.Storage;
using Application.Repositories;
using Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Images.UploadImage
{
    public class UploadImageCommandHandler : IRequestHandler<UploadImageCommandRequest, UploadImageCommandResponse>
    {
        IImageWriteRepository _imageWriteRepository;
        IPostReadRepository _postReadRepository;
        IStorageService _storageService;

        public UploadImageCommandHandler(IImageWriteRepository imageWriteRepository, IPostReadRepository postReadRepository, IStorageService storageService)
        {
            _imageWriteRepository = imageWriteRepository;
            _postReadRepository = postReadRepository;
            _storageService = storageService;
        }

        public async Task<UploadImageCommandResponse> Handle(UploadImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string path)> result = await _storageService.UploadAsync("images", request.FormFiles);

            Domain.Entities.Post post = await _postReadRepository.GetByIdAsync(request.Id);

            

            await _imageWriteRepository.AddRangeAsync(result.Select(p => new Image()
            {
                FileName = p.fileName,
                Path = p.path,
                Storage = _storageService.StorageName,
                Post = post
            }).ToList());

            await _imageWriteRepository.SaveAsync();
            return new();
        }
    }
}
