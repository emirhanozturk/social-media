using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.Posts
{
    public class UpdatePostDto
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
    }
}
