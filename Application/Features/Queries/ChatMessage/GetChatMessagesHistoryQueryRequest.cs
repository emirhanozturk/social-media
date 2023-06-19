using MediatR;

namespace Application.Features.Queries.ChatMessage
{
    public class GetChatMessagesHistoryQueryRequest : IRequest<GetChatMessagesHistoryQueryResponse>
    {
        public string OtherUserId { get; set; }

    }
}