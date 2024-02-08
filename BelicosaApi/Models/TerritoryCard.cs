using System.Text.Json.Serialization;

namespace BelicosaApi.Models
{
    public class TerritoryCard
    {
        public int Id { get; set; }
        public BelicosaGame Game { get; set; } = null!;
        public Territory Territory { get; set; } = null!;
        public Shape Shape { get; set; }
        public Player? Holder { get; set; }
    }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum Shape
    {
        Triangle,
        Square,
        Circle
    }
}
