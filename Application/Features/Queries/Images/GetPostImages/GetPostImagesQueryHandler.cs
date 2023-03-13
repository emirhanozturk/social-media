using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Images.GetPostImages
{
    public class GetPostImagesQueryHandler : IRequestHandler<GetPostImagesQueryRequest, GetPostImagesQueryResponse>
    {
        public Task<GetPostImagesQueryResponse> Handle(GetPostImagesQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
