using Application.Abstracts.Services;
using Application.Dtos;
using MediatR;

namespace Application.Features.Commands.AppUsers.RefreshToken
{
    public class RefreshTokenLoginCommandHandler : IRequestHandler<RefreshTokenLoginCommandRequest, RefreshTokenLoginCommandResponse>
    {
        private readonly IAuthService _authService;

        public RefreshTokenLoginCommandHandler(IAuthService authService)
        {
            _authService = authService;
        }

        public async Task<RefreshTokenLoginCommandResponse> Handle(RefreshTokenLoginCommandRequest request, CancellationToken cancellationToken)
        {
            
            Token token = await _authService.RefreshTokenLoginAsync(request.RefreshToken);
            return new() { Token = token};
        }
    }
}
