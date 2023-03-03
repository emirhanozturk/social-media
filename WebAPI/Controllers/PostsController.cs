﻿using Application.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get() {
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            
        }
    }
}