using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Posts.GetPostById
{
    public class GetPostByIdQueryRequest : IRequest<GetPostByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
