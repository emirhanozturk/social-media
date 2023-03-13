using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.Posts.GetPostById
{
    public class GetPostByIdQueryResponse
    {
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
