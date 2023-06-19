using Application.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Queries.ChatMessage
{
    public class GetChatMessagesHistoryQueryHandler : IRequestHandler<GetChatMessagesHistoryQueryRequest, GetChatMessagesHistoryQueryResponse>
    {
        public Task<GetChatMessagesHistoryQueryResponse> Handle(GetChatMessagesHistoryQueryRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
