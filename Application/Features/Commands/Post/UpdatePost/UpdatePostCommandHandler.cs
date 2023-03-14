using Application.Repositories;
using Domain.Entities;
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
        private readonly IPostReadRepository _postReadRepository;

        public UpdatePostCommandHandler(IPostWriteRepository postWriteRepository, IPostReadRepository postReadRepository)
        {
            _postWriteRepository = postWriteRepository;
            _postReadRepository = postReadRepository;
        }

        public async Task<UpdatePostCommandResponse> Handle(UpdatePostCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Post post = await _postReadRepository.GetByIdAsync(request.UpdatePostDto.Id);
            post.Description = request.UpdatePostDto.Description;
            post.Title = request.UpdatePostDto.Title;

            await _postWriteRepository.SaveAsync();

            return new();
        }
    }
}
