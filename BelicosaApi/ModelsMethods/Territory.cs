namespace BelicosaApi.Models
{
    public partial class Territory
    {
        public Player? GetOccupant()
        {
            return OccupyingPlayer;
        }

        public void AddBorder(Territory territory)
        {
            TerritoryTerritory territoriesRelation = new TerritoryTerritory
            {
                Territory = this,
                TerritoryTo = territory
            };

            CanAttack.Add(territoriesRelation);
        }

    }
}
