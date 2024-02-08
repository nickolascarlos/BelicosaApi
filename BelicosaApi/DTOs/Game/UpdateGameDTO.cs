using BelicosaApi.Models;

namespace BelicosaApi.DTOs.Game
{
    public class UpdateGameDTO
    {
        public GameStatus? Status { get; set; }
        public string? Title { get; set; }
    }

}