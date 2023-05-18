using Application.Abstracts.Hubs;
using Application.Repositories;
using Domain.Entities.Identity;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Commands.Post.CreatePost
{
    public class CreatePostCommandHandler : IRequestHandler<CreatePostCommandRequest, CreatePostCommandResponse>
    {
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IPostHubService _postHubService;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly UserManager<AppUser> _userManager;

        public CreatePostCommandHandler(IPostWriteRepository postWriteRepository, IPostHubService postHubService, IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _postWriteRepository = postWriteRepository;
            _postHubService = postHubService;
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }

        public async Task<CreatePostCommandResponse> Handle(CreatePostCommandRequest request, CancellationToken cancellationToken)
        {
            var username = _httpContextAccessor?.HttpContext?.User?.Identity?.Name;
            if(!string.IsNullOrEmpty(username) )
            {
               AppUser? user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);
            await _postWriteRepository.AddAsync(new()
            {
                User = user,
                Description = request.Description,
                Title = request.Title
            });
            await _postWriteRepository.SaveAsync();
            }

            await _postHubService.PostAddedMessageAsync($"{request.Description} post paylaşıldı.");
            return new();
        }
    }
}
