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

    public enum Shape
    {
        Triangle,
        Square,
        Circle
    }
}
