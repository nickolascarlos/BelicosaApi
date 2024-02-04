namespace BelicosaApi.Models
{
    public class Player
    {
        public int Id { get; set; }
        public BelicosaGame Game { get; set; } = null!;
        public User User { get; set; } = null!;
        public Color ArmyColor { get; set; }
        public int AvailableFreeDistributionTroops { get; set; } = 0;
    }

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
