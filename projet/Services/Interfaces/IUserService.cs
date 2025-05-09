using projet.Models;

namespace projet.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersWithRolesAsync();
        Task<User?> GetUserWithRolesAsync(int userId);
        Task<IEnumerable<User>> GetAllUsersWithPostsAsync();
        Task<User?> GetUserWithPostsAsync(int userId);
        Task<User> CreateUserAsync(User user);
        Task<User?> UpdateUserAsync(int id, User updatedUser);
        Task<bool> DeleteUserAsync(int id);
        Task<User?> GetUserByEmailAsync(string username);
    }

}
