using Application.Repositories;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Images.RemoveImage
{
    public class RemoveImageCommandHandler : IRequestHandler<RemoveImageCommandRequest, RemoveImageCommandResponse>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IPostWriteRepository _postWriteRepository;

        public RemoveImageCommandHandler(IPostReadRepository postReadRepository, IPostWriteRepository postWriteRepository)
        {
            _postReadRepository = postReadRepository;
            _postWriteRepository = postWriteRepository;
        }

        public async Task<RemoveImageCommandResponse> Handle(RemoveImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Post? post = await _postReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));

            Domain.Entities.Image? image = post?.Images.FirstOrDefault(p => p.Id == Guid.Parse(request.ImageId));
            if(image != null)
                post.Images.Remove(image);
            await _postWriteRepository.SaveAsync();
            return new();
        }
    }
}
