using System.Text.Json.Serialization;
namespace projet.Models
{
   public class User
{
    public int Id { get; set; }
    public string Username { get; set; }=null!;
    public string Email { get; set; }=null!;
    public string Password { get; set; }=null!;

    public ICollection<Post> Posts { get; set; } = new List<Post>();
   
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}

}
