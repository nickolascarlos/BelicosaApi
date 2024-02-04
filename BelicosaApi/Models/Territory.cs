namespace BelicosaApi.Models
{
    public class Territory
    {
        public int Id { get; set; }
        public BelicosaGame Game { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Player OccupyingPlayer { get; set; } = null!;
        public List<Territory> BorderTerritories { get; set; } = null!;
        public int TroopCount { get; set; }

    }
}
