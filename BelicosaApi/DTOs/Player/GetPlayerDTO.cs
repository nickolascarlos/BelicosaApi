using BelicosaApi.Models;

namespace BelicosaApi.DTOs.Player
{
    public class GetPlayerDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public User User { get; set; } = null!;
        public Color ArmyColor { get; set; }
        public int AvailableFreeDistributionTroops { get; set; }
    }
}
