using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace projet.Models
{
    public class Post
    {
        public int Id { get; set; }
        [StringLength(100,MinimumLength = 8)]
        public string Content { get; set; } = null!;
        public int UserId { get; set; }
 [JsonIgnore] 
        public User? User { get; set; }
    }
}
