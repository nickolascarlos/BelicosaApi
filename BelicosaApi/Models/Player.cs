using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;
using System.Text.Json.Serialization;

namespace BelicosaApi.Models
{
    public partial class Player
    {
        public int Id { get; set; }
        public int GameId { get; set; }

        [JsonIgnore]
        public BelicosaGame Game { get; set; } = null!;
        public string UserId { get; set; }

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public IdentityUser User { get; set; } = null!;
        public Color ArmyColor { get; set; }
        public int AvailableFreeDistributionTroops { get; set; } = 0;
        public List<ContinentalTroopsAvailability> AvailableContinentalDistributionTroops { get; set;  } = new();
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Color
    {
        Red,
        Green,
        Blue,
        Yellow,
        White,
        Black
    }
}
