using Microsoft.AspNetCore.Mvc;
using projet.Models;
using projet.Services.Interfaces;

namespace projet.Controllers
{
   [ApiController]
[Route("api/posts")]
public class PostsController : ControllerBase
{
    private readonly IPostService _postService;

    public PostsController(IPostService postService)
    {
        _postService = postService;
    }

    // GET: api/posts
    [HttpGet]
    public async Task<IActionResult> GetAllPosts()
    {
        var posts = await _postService.GetAllPostsAsync();
        return Ok(posts);
    }

    // GET: api/posts/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetPostById(int id)
    {
        var post = await _postService.GetPostByIdAsync(id);
        if (post == null) return NotFound();
        return Ok(post);
    }

    // GET: api/posts/user/{userId}
    [HttpGet("user/{userId}")]
    public async Task<IActionResult> GetPostsByUserId(int userId)
    {
        var posts = await _postService.GetPostsByUserIdAsync(userId);
        return Ok(posts);
    }

    // POST: api/posts
    [HttpPost]
    public async Task<IActionResult> CreatePost([FromBody] Post post)
    {
        var createdPost = await _postService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
    }

    // PUT: api/posts/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdatePost(int id, [FromBody] Post updatedPost)
    {
        var post = await _postService.UpdatePostAsync(id, updatedPost);
        if (post == null) return NotFound();
        return Ok(post);
    }

    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        var success = await _postService.DeletePostAsync(id);
        return success ? NoContent() : NotFound();
    }
}


}
