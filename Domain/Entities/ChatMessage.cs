using Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ChatMessage : BaseEntity
    {
        public string SenderId { get; set; } = default!;

        public string ReceiverId { get; set; } = default!;

        public string Message { get; set; } = default!;

        public DateTime MessageTime { get; set; } = default!;
    }
}
