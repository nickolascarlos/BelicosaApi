using System.ComponentModel.DataAnnotations.Schema;

namespace BelicosaApi.Models
{
    public partial class Territory
    {
        public int Id { get; set; }
        public BelicosaGame Game { get; set; } = null!;
        public string Name { get; set; } = null!;
        public Player? OccupyingPlayer { get; set; }
        public List<TerritoryTerritory> TerritoryRelations { get; set; } = new();
        public int TroopCount { get; set; }


        // Bi-directional graph
        // These two following lists (instead of just one) is
        // required for Entity Framework to work
        public List<TerritoryTerritory> CanAttack { get; set; } = new();
        public List<TerritoryTerritory> MayBeAttackedBy { get; set; } = new();
    }
}
