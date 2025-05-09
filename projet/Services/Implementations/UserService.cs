using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.Models;
using projet.Services.Interfaces;

namespace projet.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly IRoleService _roleService;
        public UserService(ApplicationDbContext context, IRoleService roleService)
        {
            _context = context;

              _roleService = roleService;
        }

        public async Task<IEnumerable<User>> GetAllUsersWithRolesAsync()
    {
        return await _context.Users.Include(u => u.Roles).ToListAsync();
    }

    public async Task<User?> GetUserWithRolesAsync(int userId)
    {
        return await _context.Users.Include(u => u.Roles).FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<IEnumerable<User>> GetAllUsersWithPostsAsync()
    {
        return await _context.Users.Include(u => u.Posts).ToListAsync();
    }

    public async Task<User?> GetUserWithPostsAsync(int userId)
    {
        return await _context.Users.Include(u => u.Posts).FirstOrDefaultAsync(u => u.Id == userId);
    }

    public async Task<User> CreateUserAsync(User user)
    {
         user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password); 
      var usersCount = await _context.Users.CountAsync();
        bool isFirstUser = usersCount == 0;
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
      
        if(isFirstUser){
          var roleExists = await _roleService.RoleExistsAsync("Admin");
        Role role;
        if (!roleExists)
        {
            role = await _roleService.CreateRoleAsync(new Role { Name = "Admin" });
        }
        else
        {
            role = await _roleService.GetRoleByIdAsync(1); // Assuming 1 = Admin and 2 = User
        }

        // Assign the role to the user
        var result = await _roleService.AssignRoleToUserAsync(user.Id, role!.Id);
        if (!result)
        {
            throw new Exception("Failed to assign role to user.");
        }}
        return user;
    }

    public async Task<User?> UpdateUserAsync(int id, User updatedUser)
    {

        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        user.Password = BCrypt.Net.BCrypt.HashPassword( updatedUser.Password); 

        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

     public async Task<User?> GetUserByEmailAsync(string username)
    {
        return await _context.Users.FirstOrDefaultAsync(u => u.Email == username);
    }

}
}