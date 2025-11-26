using BlogPlatform.Core.Entities;
using BlogPlatform.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace BlogPlatform.API.Controllers
{
    [ApiController]
    [Route("api/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly ICommentRepository _commentRepository;
        private readonly IPostRepository _postRepository;

    public CommentsController(ICommentRepository commentRepository, IPostRepository postRepository)
        {
            _commentRepository = commentRepository;
            _postRepository = postRepository;
        }

    [HttpGet("/api/posts/{postId}/comments")]
        public async Task<IActionResult> GetCommentsByPostId(int postId)
        {
            if (!await _postRepository.ExistsAsync(postId))
            {
                return NotFound($"Post with ID {postId} not found.");
            }

            var comments = await _commentRepository.GetCommentsByPostIdAsync(postId);
            return Ok(comments); // Returns 200 OK
        }

        [HttpPost("/api/posts/{postId}/comments")]
        public async Task<IActionResult> CreateComment(int postId, [FromBody] Comment comment)
        {
            if (!await _postRepository.ExistsAsync(postId))
            {
                return NotFound($"Post with ID {postId} not found. Cannot add comment.");
            }

            comment.PostId = postId;
            comment.CreatedDate = DateTime.UtcNow;

            await _commentRepository.AddAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return CreatedAtAction(nameof(GetCommentById), new { id = comment.Id }, comment);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCommentById(int id)
        {
            var comment = await _commentRepository.GetByIdAsync(id);
            if (comment == null)
            {
                return NotFound();
            }
            return Ok(comment);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateComment(int id, [FromBody] Comment comment)
        {
            if (id != comment.Id)
            {
                return BadRequest("Comment ID in the route must match the ID in the body.");
            }

            if (!await _commentRepository.ExistsAsync(id))
            {
                return NotFound();
            }
            comment.PostId = (await _commentRepository.GetByIdAsync(id))!.PostId;
            
            await _commentRepository.UpdateAsync(comment);
            await _commentRepository.SaveChangesAsync();

            return NoContent(); // 204 error
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            if (!await _commentRepository.ExistsAsync(id))
            {
                return NotFound();
            }

            await _commentRepository.DeleteAsync(id);
            await _commentRepository.SaveChangesAsync();

            return NoContent(); // 204 error
        }
    }
}