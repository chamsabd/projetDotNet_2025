using projet.Models;

namespace projet.Services.Interfaces
{
    public interface IPostService
    {
        Task<IEnumerable<Post>> GetAllPostsAsync();
        Task<Post?> GetPostByIdAsync(int id);
        Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId);
        Task<Post> CreatePostAsync(Post post);
        Task<Post?> UpdatePostAsync(int id, Post updatedPost);
        Task<bool> DeletePostAsync(int id);
    }

}
