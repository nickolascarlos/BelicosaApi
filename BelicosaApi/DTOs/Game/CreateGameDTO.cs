using System.ComponentModel.DataAnnotations;

namespace BelicosaApi.DTOs.Game
{
    public class CreateGameDTO
    {
        [Required]
        [StringLength(30, MinimumLength = 5)]
        public string Title { get; set; } = string.Empty;
    }
}
