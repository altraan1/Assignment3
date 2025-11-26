using Microsoft.EntityFrameworkCore;
using BlogPlatform.Core.Interfaces;
using BlogPlatform.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System;

namespace BlogPlatform.API.Controllers
{
    [ApiController]
    [Route("api/posts")]
    public class PostsController : ControllerBase
    {
        private readonly IPostRepository _postRepository;

        public PostsController(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPosts()
        {
            var posts = await _postRepository.GetAllAsync();
            return Ok(posts); 
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostById(int id)
        {
            var post = await _postRepository.GetByIdAsync(id);
            if (post == null)
            {
                return NotFound();
            }
            return Ok(post);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePost([FromBody] Post post)
        {
            post.Author = "admin";
            post.Comments.Clear();
            post.CreatedDate = DateTime.UtcNow;
            await _postRepository.AddAsync(post);
            await _postRepository.SaveChangesAsync();
            return CreatedAtAction(nameof(GetPostById), new { id = post.Id }, post);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePost(int id, [FromBody] Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            if (!await _postRepository.ExistsAsync(id))
            {
                return NotFound();
            }
            post.Author = "admin";
            await _postRepository.UpdateAsync(post);
            await _postRepository.SaveChangesAsync();
            return NoContent();
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> PartialUpdatePost(int id, [FromBody] Post postUpdate)
        {
            var existingPost = await _postRepository.GetByIdAsync(id);
            if (existingPost == null)
            {
                return NotFound(); 
            }
            if (!string.IsNullOrWhiteSpace(postUpdate.Title))
            {
                existingPost.Title = postUpdate.Title;
            }

            if (!string.IsNullOrWhiteSpace(postUpdate.Content))
            {
                existingPost.Content = postUpdate.Content;
            }
            await _postRepository.UpdatePartialAsync(existingPost); 

            await _postRepository.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(int id)
        {
            if (!await _postRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _postRepository.DeleteAsync(id);
            await _postRepository.SaveChangesAsync();

            return NoContent();
        }
    }
}