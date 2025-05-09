using Microsoft.AspNetCore.Mvc;
using projet.Models;
using projet.Services.Interfaces;

namespace projet.Controllers
{
  [ApiController]
[Route("api/users")]
public class UsersController : ControllerBase
{
    private readonly IUserService _userService;

    public UsersController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/users/with-roles
    [HttpGet("with-roles")]
    public async Task<IActionResult> GetAllUsersWithRoles()
    {
        var users = await _userService.GetAllUsersWithRolesAsync();
        return Ok(users);
    }

    // GET: api/users/{id}/with-roles
    [HttpGet("{id}/with-roles")]
    public async Task<IActionResult> GetUserWithRoles(int id)
    {
        var user = await _userService.GetUserWithRolesAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // GET: api/users/with-posts
    [HttpGet("with-posts")]
    public async Task<IActionResult> GetAllUsersWithPosts()
    {
        var users = await _userService.GetAllUsersWithPostsAsync();
        return Ok(users);
    }

    // GET: api/users/{id}/with-posts
    [HttpGet("{id}/with-posts")]
    public async Task<IActionResult> GetUserWithPosts(int id)
    {
        var user = await _userService.GetUserWithPostsAsync(id);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // POST: api/users
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] User user)
    {
        var createdUser = await _userService.CreateUserAsync(user);
        return CreatedAtAction(nameof(GetUserWithRoles), new { id = createdUser.Id }, createdUser);
    }

    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] User updatedUser)
    {
        var user = await _userService.UpdateUserAsync(id, updatedUser);
        if (user == null) return NotFound();
        return Ok(user);
    }

    // DELETE: api/users/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var success = await _userService.DeleteUserAsync(id);
        return success ? NoContent() : NotFound();
    }
}

}
