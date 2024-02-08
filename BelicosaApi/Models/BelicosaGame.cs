using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BelicosaApi.Models
{
    public partial class BelicosaGame
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IdentityUser? Owner { get; set; } = null!;

        public DateTime? StartTime { get; set; }
        public DateTime? FinishTime { get; set; }
        public string Title { get; set; } = string.Empty;
        public int CardExchangeCount { get; set; } = 0;
        public GameStatus Status { get; set; } = GameStatus.Preparing;
        
        //[NotMapped]
        //public List<int> PlayersIds { 
        //    get {
        //        return Players.Select(p => p.Id).ToList();
        //    }
        //}

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public List<Player> Players { get; private set; } = new List<Player>();
        public List<TerritoryCard> TerritoryCards { get; set; } = new List<TerritoryCard>();
        public List<Territory> Territories { get; set; } = new List<Territory>();
        public List<Continent> Continents { get; private set; } = new List<Continent>();
        public List<Color> AvailableColors { get; private set; } = typeof(Color).GetEnumValues().Cast<Color>().ToList();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum GameStatus
    {
        Preparing,
        Started,
        Finished
    }
}
