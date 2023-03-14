using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Post.RemovePost
{
    public class RemovePostCommandRequest : IRequest<RemovePostCommandResponse>
    {
        public string Id { get; set; }
    }
}
