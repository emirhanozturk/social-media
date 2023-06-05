using Application.Dtos.Posts;
using Application.Features.Queries.Posts.GetAllPost;
using Application.Features.Queries.Posts.GetPostById;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstracts.Services
{
    public interface IPostService
    {
        Task CreatePost(CreatePostDto createPostDto);
        Task UpdatePost(UpdatePostDto updatePostDto);
        Task DeletePost(string postId);
        Task<GetAllPostQueryResponse> GetAllPosts();
        Task<GetPostByIdQueryResponse> GetPostById(string postId);
    }
}
