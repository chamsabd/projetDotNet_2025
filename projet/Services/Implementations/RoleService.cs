using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.Models;
using projet.Services.Interfaces;

namespace projet.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;
        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

       public async Task<Role> CreateRoleAsync(Role role)
    {
        _context.Roles.Add(role);
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<Role?> GetRoleByIdAsync(int id)
    {
        return await _context.Roles.Include(r => r.Users).FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<IEnumerable<Role>> GetAllRolesAsync()
    {
        return await _context.Roles.Include(r => r.Users).ToListAsync();
    }

    public async Task<Role?> UpdateRoleAsync(int id, Role updatedRole)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) return null;

        role.Name = updatedRole.Name;
        await _context.SaveChangesAsync();
        return role;
    }

    public async Task<bool> DeleteRoleAsync(int id)
    {
        var role = await _context.Roles.FindAsync(id);
        if (role == null) return false;

        _context.Roles.Remove(role);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> AssignRoleToUserAsync(int userId, int roleId)
    {
        var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
        var role = await _context.Roles.FindAsync(roleId);

        if (user == null || role == null) return false;

        if (!user.Roles.Contains(role))
        {
            user.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        return true;
    }

    public async Task<bool> RemoveRoleFromUserAsync(int userId, int roleId)
    {
        var user = await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
        var role = await _context.Roles.FindAsync(roleId);

        if (user == null || role == null) return false;

        if (user.Roles.Contains(role))
        {
            user.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        return true;
    }
}
}
