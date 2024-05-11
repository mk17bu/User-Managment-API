using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using User_Management_API.Entities;
using User_Management_API.Models;
using User_Management_API.Services;

namespace User_Management_API.Controllers;

[ApiController]
[Authorize]
[Route("api/posts")]
public class PostsController(IPostRepository postRepository, ILogger<PostsController> logger) : ControllerBase
{
    private readonly IPostRepository _postRepository = postRepository ?? throw new ArgumentNullException(nameof(postRepository));
    private readonly ILogger<PostsController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    
    [HttpGet]
    public async Task<IActionResult> GetPosts()
    {
        var posts = await _postRepository.GetPostsAsync();
        return Ok(posts);
    }
    
    [HttpGet("{postId}")]
    public async Task<IActionResult> GetPostById(int postId)
    {
        var post = await _postRepository.GetPostByIdAsync(postId);
        if (post == null)
        {
            _logger.LogInformation($"Post with ID: {postId} can't be found.");
            return NotFound();
        }

        return Ok(post);
    }
    
    [HttpPost]
    public async Task<ActionResult<Post>> CreatePost(PostForCreationDto postForCreationDto)
    {
        var newPost = await postRepository.CreatePostAsync(postForCreationDto);
        return CreatedAtAction(nameof(GetPostById), new { id = newPost.Id }, newPost);
    }
    
    [HttpPut("{postId}")]
    public async Task<ActionResult> UpdatePost(int postId, PostForUpdateDto postForUpdateDto)
    {
        await _postRepository.UpdatePostAsync(postId, postForUpdateDto);
        return NoContent();
    }

    [HttpDelete("{postId}")]
    public async Task<IActionResult> DeletePost(int postId)
    {
        await _postRepository.DeletePostAsync(postId);
        return NoContent();
    }
}