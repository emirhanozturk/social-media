using Application.Repositories;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Images.GetPostImages
{
    public class GetPostImagesQueryHandler : IRequestHandler<GetPostImagesQueryRequest, List<GetPostImagesQueryResponse>>
    {
        private readonly IPostReadRepository _postReadRepository;
        private readonly IConfiguration _configuration;

        public GetPostImagesQueryHandler(IConfiguration configuration, IPostReadRepository postReadRepository)
        {
            _configuration = configuration;
            _postReadRepository = postReadRepository;
        }

        public async Task<List<GetPostImagesQueryResponse>> Handle(GetPostImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Post? post = await _postReadRepository.Table.Include(p => p.Images).FirstOrDefaultAsync(p => p.Id == Guid.Parse(request.Id));
            return post.Images.Select(p => new GetPostImagesQueryResponse
            {
                Id = p.Id,
                Path = $"{_configuration["StorageUrl"]}/{p.Path}",
                FileName = p.FileName
            }).ToList();
        }
    }
}
