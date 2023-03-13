using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Post.UpdatePost
{
    public class UpdatePostCommandHandler : IRequestHandler<UpdatePostCommandRequest, UpdatePostCommandResponse>
    {
        private readonly IPostWriteRepository _postWriteRepository;

        public UpdatePostCommandHandler(IPostWriteRepository postWriteRepository)
        {
            _postWriteRepository = postWriteRepository;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            await _postWriteRepository.AddAsync(new()
            {
                Description = request.CreatePostDto.Description,
                Title = request.CreatePostDto.Title
            });
            await _postWriteRepository.SaveAsync();
            return new();
        }
    }
}
