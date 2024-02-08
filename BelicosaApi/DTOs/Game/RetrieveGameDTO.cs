using BelicosaApi.Models;
using System.Text.Json.Serialization;

namespace BelicosaApi.DTOs.Game
{
    public class RetrieveGameDTO
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public string Title { get; set; } = string.Empty;
        public int CardExchangeCount { get; set; }
        public GameStatus Status { get; set; }
        public List<RetrieveGamePlayerDTO> Players { get; private set; } = null!;
        //public List<TerritoryCard> TerritoryCards { get; set; }
        //public List<Territory> Territories { get; set; } 
        //public List<Continent> Continents { get; private set; }
    }

    public class RetrieveGamePlayerDTO
    {
        public int Id { get; set; }
        // public User User { get; set; } = null!;
        public Color ArmyColor { get; set; }
        public int AvailableFreeDistributionTroops { get; set; } = 0;
    }
}
