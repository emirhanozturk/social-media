using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Post.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
    {
        private readonly IPostWriteRepository _postWriteRepository;

        public CreatePostCommandHandler(IPostWriteRepository postWriteRepository)
        {
            _postWriteRepository = postWriteRepository;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
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
