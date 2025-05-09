using projet.Models;

namespace projet.Services.Interfaces
{
    public interface IRoleService
    {
        Task<Role> CreateRoleAsync(Role role);
        Task<Role?> GetRoleByIdAsync(int id);
        Task<IEnumerable<Role>> GetAllRolesAsync();
        Task<Role?> UpdateRoleAsync(int id, Role updatedRole);
        Task<bool> DeleteRoleAsync(int id);
        Task<bool> AssignRoleToUserAsync(int userId, int roleId);
        Task<bool> RemoveRoleFromUserAsync(int userId, int roleId);
          Task<bool> RoleExistsAsync(string roleName);
    }

}
