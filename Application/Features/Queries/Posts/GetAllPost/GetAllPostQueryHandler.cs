using Application.Repositories;
using Application.RequestParamaters;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Posts.GetAllPost
{
    public class GetAllPostQueryHandler : IRequestHandler<GetAllPostQueryRequest, GetAllPostQueryResponse>
    {
        private readonly IPostReadRepository _postReadRepository;

        public GetAllPostQueryHandler(IPostReadRepository postReadRepository)
        {
            _postReadRepository = postReadRepository;
        }

        public async Task<GetAllPostQueryResponse> Handle(GetAllPostQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _postReadRepository.GetAll(false).Count();
            var posts = _postReadRepository.GetAll(false).Skip(request.Page * request.Size).Take(request.Size).Select(p => new
            {
                p.Id,
                p.Title,
                p.Description,
                p.CreatedDate
            }).ToList();

            return new()
            {
                Posts = posts,
                Count = totalCount
            };
        }
    }
}
