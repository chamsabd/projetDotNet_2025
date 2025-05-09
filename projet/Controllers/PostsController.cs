using Microsoft.AspNetCore.Mvc;
using projet.Models;
using projet.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;


namespace projet.Controllers
{
   [ApiController]
[Route("api/posts")]
[Authorize]
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
          var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
     if (userIdClaim == null)
        return Unauthorized();
    if (post.UserId !=  int.Parse(userIdClaim.Value))
        return Unauthorized("Vous n'êtes pas autorisé à ajouter ce post.");

    int userId = int.Parse(userIdClaim.Value);
    post.UserId = userId;
        var createdPost = await _postService.CreatePostAsync(post);
        return CreatedAtAction(nameof(GetPostById), new { id = createdPost.Id }, createdPost);
    }

  [HttpPut("{id}")]
[Authorize]
public async Task<IActionResult> UpdatePost(int id, [FromBody] Post updatedPost)
{
    // Récupérer l'ID du user authentifié
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
        return Unauthorized();

    int userId = int.Parse(userIdClaim.Value);

    // Vérifier si le post appartient à cet utilisateur
    var existingPost = await _postService.GetPostByIdAsync(id);
    if (existingPost == null)
        return NotFound();

    if (existingPost.UserId != userId && updatedPost.UserId != userId)
        return Unauthorized("Vous n'êtes pas autorisé à modifier ce post.");

    // Continuer la mise à jour
    var post = await _postService.UpdatePostAsync(id, updatedPost);
    return Ok(post);
}


    // DELETE: api/posts/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeletePost(int id)
    {
        
    // Récupérer l'ID du user authentifié
    var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
    if (userIdClaim == null)
        return Unauthorized();

    int userId = int.Parse(userIdClaim.Value);

    // Vérifier si le post appartient à cet utilisateur
    var existingPost = await _postService.GetPostByIdAsync(id);
    if (existingPost == null)
        return NotFound();

    if (existingPost.UserId != userId)
        return Unauthorized("Vous n'êtes pas autorisé à supprimer ce post.");

        var success = await _postService.DeletePostAsync(id);
        return success ? NoContent() : NotFound();
    }
}


}
