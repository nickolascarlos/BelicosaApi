using BelicosaApi.Models;
using Microsoft.AspNetCore.Identity;

namespace BelicosaApi.DTOs.Player
{
    public class RetrievePlayerDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public IdentityUser User { get; set; } = null!;
        public Color ArmyColor { get; set; }
        public int AvailableFreeDistributionTroops { get; set; }
    }
}
