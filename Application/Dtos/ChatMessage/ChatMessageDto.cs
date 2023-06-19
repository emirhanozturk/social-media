using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dtos.ChatMessage
{
    public class ChatMessageDto
    {
          public string Id { get; set; } = default!;
          public string SenderId { get; set; } = default!;
          public string ReceiverId { get; set; } = default!;
          public string Message { get; set; } = default!;
          public DateTime MessageTime { get; set; } = default!;
    }
}
