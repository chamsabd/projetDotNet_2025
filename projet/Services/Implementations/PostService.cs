using Microsoft.EntityFrameworkCore;
using projet.Data;
using projet.Models;
using projet.Services.Interfaces;

namespace projet.Services.Implementations
{
   public class PostService : IPostService
{
    private readonly ApplicationDbContext _context;

    public PostService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Post>> GetAllPostsAsync()
    {
        return await _context.Posts.Include(p => p.User).ToListAsync();
    }

    public async Task<Post?> GetPostByIdAsync(int id)
    {
        return await _context.Posts.Include(p => p.User).FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<IEnumerable<Post>> GetPostsByUserIdAsync(int userId)
    {
        return await _context.Posts.Where(p => p.UserId == userId).ToListAsync();
    }

    public async Task<Post> CreatePostAsync(Post post)
    {
        _context.Posts.Add(post);
        await _context.SaveChangesAsync();
        return post;
    }

    public async Task<Post?> UpdatePostAsync(int id, Post updatedPost)
    {
        var existing = await _context.Posts.FindAsync(id);
        if (existing == null) return null;

        existing.Content = updatedPost.Content;
        existing.UserId = updatedPost.UserId;

        await _context.SaveChangesAsync();
        return existing;
    }

    public async Task<bool> DeletePostAsync(int id)
    {
        var post = await _context.Posts.FindAsync(id);
        if (post == null) return false;

        _context.Posts.Remove(post);
        await _context.SaveChangesAsync();
        return true;
    }
}

    }


