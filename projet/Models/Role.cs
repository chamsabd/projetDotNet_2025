using System.Text.Json.Serialization;
namespace projet.Models
{
   
public class Role
{
    public int Id { get; set; }
    public required string Name { get; set; }
 [JsonIgnore] 
    public ICollection<User> Users { get; set; }= new List<User>(); // visible
}
}
