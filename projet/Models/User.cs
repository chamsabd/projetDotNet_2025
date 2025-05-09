using System.Text.Json.Serialization;
namespace projet.Models
{
   public class User
{
    public int Id { get; set; }
    public string Username { get; set; }=null!;
    public string Email { get; set; }=null!;
    public string Password { get; set; }=null!;
 [JsonIgnore] 
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    [JsonIgnore] 
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}

}
