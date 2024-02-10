using BelicosaApi.DTOs.User;
using BelicosaApi.Models;

namespace BelicosaApi.DTOs.Player
{
    public class NoGameId_RetrievePlayerDTO
    {
        public int Id { get; set; }
        public RetrieveUserDTO User { get; set; } = null!;
        public Color ArmyColor { get; set; }
        public int AvailableFreeDistributionTroops { get; set; }
    }
}
