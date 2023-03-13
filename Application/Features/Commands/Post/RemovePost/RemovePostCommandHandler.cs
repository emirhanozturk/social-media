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
        public Task<RemovePostCommandResponse> Handle(RemovePostCommandRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
