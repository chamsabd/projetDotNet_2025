using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace projet.Models
{
   public class User
{
    public int Id { get; set; }
     [StringLength(100,MinimumLength = 3)]
    public string Username { get; set; }=null!;
    [EmailAddress]
    public string Email { get; set; }=null!;
     [StringLength(100,MinimumLength = 3)]
    public string Password { get; set; }=null!;
 [JsonIgnore] 
    public ICollection<Post> Posts { get; set; } = new List<Post>();
    [JsonIgnore] 
    public ICollection<Role> Roles { get; set; } = new List<Role>();
}

}
