using Microsoft.AspNetCore.Mvc;
using projet.Models;
using projet.Services.Interfaces;

namespace projet.Controllers
{
  [ApiController]
[Route("api/roles")]
public class RolesController : ControllerBase
{
    private readonly IRoleService _roleService;

    public RolesController(IRoleService roleService)
    {
        _roleService = roleService;
    }

    // POST: api/roles
    [HttpPost]
    public async Task<IActionResult> CreateRole([FromBody] Role role)
    {
        var created = await _roleService.CreateRoleAsync(role);
        return CreatedAtAction(nameof(GetRole), new { id = created.Id }, created);
    }

    // GET: api/roles/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetRole(int id)
    {
        var role = await _roleService.GetRoleByIdAsync(id);
        if (role == null) return NotFound();
        return Ok(role);
    }

    // GET: api/roles
    [HttpGet]
    public async Task<IActionResult> GetAllRoles()
    {
        var roles = await _roleService.GetAllRolesAsync();
        return Ok(roles);
    }

    // PUT: api/roles/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateRole(int id, [FromBody] Role updatedRole)
    {
        var role = await _roleService.UpdateRoleAsync(id, updatedRole);
        if (role == null) return NotFound();
        return Ok(role);
    }

    // DELETE: api/roles/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteRole(int id)
    {
        var success = await _roleService.DeleteRoleAsync(id);
        return success ? NoContent() : NotFound();
    }

    // POST: api/roles/assign
    [HttpPost("assign")]
    public async Task<IActionResult> AssignRoleToUser([FromQuery] int userId, [FromQuery] int roleId)
    {
        var success = await _roleService.AssignRoleToUserAsync(userId, roleId);
        return success ? Ok() : BadRequest();
    }

    // POST: api/roles/remove
    [HttpPost("remove")]
    public async Task<IActionResult> RemoveRoleFromUser([FromQuery] int userId, [FromQuery] int roleId)
    {
        var success = await _roleService.RemoveRoleFromUserAsync(userId, roleId);
        return success ? Ok() : BadRequest();
    }
}


}
