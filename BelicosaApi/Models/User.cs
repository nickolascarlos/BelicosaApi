using System.Text.Json.Serialization;

namespace BelicosaApi.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = null!;
        [JsonIgnore]
        public string Password { get; set; } = null!;
    }
}
