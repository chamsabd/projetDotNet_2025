using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.Models;
using projet.Services.Interfaces;

namespace projet.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        public UserService(ApplicationDbContext context)
        {
            _context = context;
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
        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User?> UpdateUserAsync(int id, User updatedUser)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Username = updatedUser.Username;
        user.Email = updatedUser.Email;
        user.Password = updatedUser.Password;

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
}
}