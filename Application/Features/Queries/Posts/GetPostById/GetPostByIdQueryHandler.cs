using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Posts.GetPostById
{
    public class GetPostByIdQueryHandler : IRequestHandler<GetPostByIdQueryRequest, GetPostByIdQueryResponse>
    {
        private readonly IPostReadRepository _postReadRepository;

        public GetPostByIdQueryHandler(IPostReadRepository postReadRepository)
        {
            _postReadRepository = postReadRepository;
        }

        public async Task<GetPostByIdQueryResponse> Handle(GetPostByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var response = await _postReadRepository.GetByIdAsync(request.Id);
            GetPostByIdQueryResponse result = new()
            {
                Description = response.Description,
                Title = response.Title,
            };
            return result;
        }
    }
}
