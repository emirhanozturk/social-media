using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Comment:BaseEntity
    {
        public Guid PostId { get; set; }
        public string Description { get; set; }

        public Post Post { get; set; }

    }
}
