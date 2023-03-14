using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Images.GetPostImages
{
    public  class GetPostImagesQueryResponse
    {
        public Guid Id { get; set; }
        public string Path { get; set; }
        public string FileName { get; set; }
    }
}
