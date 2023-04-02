using Application.Repositories;
using Microsoft.AspNetCore.Mvc;
using Application.Dtos;
using System.Net;
using Domain.Entities;
using Application.Abstracts.Storage;
using Microsoft.EntityFrameworkCore;
using MediatR;
using Application.Features.Queries.Posts.GetPostById;
using Application.Features.Queries.Posts.GetAllPost;
using Application.Features.Commands.Post.CreatePost;
using Application.Features.Commands.Post.UpdatePost;
using Application.Features.Commands.Post.RemovePost;
using Application.Features.Commands.Images.UploadImage;
using Application.Features.Commands.Images.RemoveImage;
using Application.Features.Queries.Images.GetPostImages;
using Microsoft.AspNetCore.Authorization;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
      

        readonly IMediator _mediator;

        public PostsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Get([FromQuery] GetAllPostQueryRequest getAllPostQueryRequest)
        {
            GetAllPostQueryResponse response = await _mediator.Send(getAllPostQueryRequest);
            return Ok(response);
           
        }

        [HttpPost]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Post(CreatePostCommandRequest createPostCommandRequest)
        {
            CreatePostCommandResponse response = await _mediator.Send(createPostCommandRequest);
            return StatusCode((int)HttpStatusCode.Created);
        }

        [HttpPut]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Put([FromBody]UpdatePostCommandRequest updatePostCommandRequest)
        {
            UpdatePostCommandResponse response = await _mediator.Send(updatePostCommandRequest);
            return Ok();
        }

        [HttpDelete("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Delete([FromRoute] RemovePostCommandRequest removePostCommandRequest)
        {
            RemovePostCommandResponse response = await _mediator.Send(removePostCommandRequest);
            return Ok();
        }

        [HttpGet("{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetById([FromRoute]GetPostByIdQueryRequest getPostByIdQueryRequest)
        {
            GetPostByIdQueryResponse result = await _mediator.Send(getPostByIdQueryRequest);
            return Ok(result);
        }

        [HttpPost("[action]")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> Upload([FromQuery] UploadImageCommandRequest uploadImageCommandRequest)
        {
            uploadImageCommandRequest.FormFiles = Request.Form.Files;
            UploadImageCommandResponse response =  await _mediator.Send(uploadImageCommandRequest);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> GetImages([FromRoute] GetPostImagesQueryRequest getPostImagesQueryRequest)
        {
            List<GetPostImagesQueryResponse>  response = await _mediator.Send(getPostImagesQueryRequest);
            return Ok(response);
        }

        [HttpDelete("[action]/{Id}")]
        [Authorize(AuthenticationSchemes = "Admin")]
        public async Task<IActionResult> DeleteImage([FromRoute] string id, [FromQuery] string imageId)
        {
            RemoveImageCommandRequest removeImageCommandRequest = new() { Id = id, ImageId = imageId };
            RemoveImageCommandResponse response = await _mediator.Send(removeImageCommandRequest);
            return Ok();

        }

    }
}
