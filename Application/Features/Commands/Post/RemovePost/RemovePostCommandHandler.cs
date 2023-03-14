using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Post.RemovePost
{
    public class RemovePostCommandHandler : IRequestHandler<RemovePostCommandRequest, RemovePostCommandResponse>
    {
        IPostWriteRepository _postWriteRepository;

        public RemovePostCommandHandler(IPostWriteRepository postWriteRepository)
        {
            _postWriteRepository = postWriteRepository;
        }

        public async Task<RemovePostCommandResponse> Handle(RemovePostCommandRequest request, CancellationToken cancellationToken)
        {
            await _postWriteRepository.RemoveAsync(request.Id);
            await _postWriteRepository.SaveAsync();
            return new();
        }
    }
}
