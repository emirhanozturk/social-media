using Application.Abstracts.Services;
using Application.Dtos.Posts;
using Application.Features.Queries.Posts.GetAllPost;
using Application.Features.Queries.Posts.GetPostById;
using Application.Repositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Services
{
    public class PostService : IPostService
    {
        private readonly IPostWriteRepository _postWriteRepository;
        private readonly IPostReadRepository _postReadRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public PostService(IPostWriteRepository postWriteRepository, IPostReadRepository postReadRepository, IHttpContextAccessor httpContextAccessor)
        {
            _postWriteRepository = postWriteRepository;
            _postReadRepository = postReadRepository;
            _httpContextAccessor = httpContextAccessor;
        }

        public Task CreatePost(CreatePostDto createPostDto)
        {
            throw new NotImplementedException();
        }

        public Task DeletePost(string postId)
        {
            throw new NotImplementedException();
        }

        public Task<GetAllPostQueryResponse> GetAllPosts()
        {
            throw new NotImplementedException();
        }

        public Task<GetPostByIdQueryResponse> GetPostById(string postId)
        {
            throw new NotImplementedException();
        }

        public Task UpdatePost(UpdatePostDto updatePostDto)
        {
            throw new NotImplementedException();
        }
    }
}
