using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace projet.Models
{
   
public class Role
{
    public int Id { get; set; }
     [StringLength(10,MinimumLength = 3)]
    public required string Name { get; set; }
 [JsonIgnore] 
    public ICollection<User> Users { get; set; }= new List<User>(); // visible
}
}
