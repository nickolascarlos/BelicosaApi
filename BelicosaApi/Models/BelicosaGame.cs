using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace BelicosaApi.Models
{
    public class BelicosaGame
    {
        public int Id { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public string Title { get; set; } = string.Empty;
        public int CardExchangeCount { get; set; } = 0;
        public GameStatus Status { get; set; } = GameStatus.Preparing;
        public List<Player> Players { get; private set; } = new List<Player>();
        public List<TerritoryCard> TerritoryCards { get; set; } = new List<TerritoryCard>();
        public List<Territory> Territories { get; set; } = new List<Territory>();
        public List<Continent> Continents { get; private set; } = new List<Continent>();
    }

    public enum GameStatus
    {
        Preparing,
        Started,
        Finished
    }
}
