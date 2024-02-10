using BelicosaApi.Exceptions;

namespace BelicosaApi.Models
{
    public partial class Player
    {
        public void DistributeContinentalTroops(Territory territory, int troopsQuantity)
        {
            Continent territoryContinent = (from continent in Game.Continents where continent.Territories.Contains(territory) select continent).Single();
            ContinentalTroopsAvailability contentAvailability = AvailableContinentalDistributionTroops.Single(a => a.Player == this && a.Continent == territoryContinent);

            if (contentAvailability is null)
            {
                contentAvailability = new ContinentalTroopsAvailability
                {
                    Player = this,
                    Continent = territoryContinent,
                    AvailableTroops = 0
                };

                AvailableContinentalDistributionTroops.Add(contentAvailability);
            }

            int availableTroops = contentAvailability.AvailableTroops;

            if (availableTroops < troopsQuantity)
            {
                throw new NotEnoughTroopsException();
            }

            if (territory.GetOccupant() != this)
            {
                throw new TerritoryNotOccupiedByPlayerException();
            }

            contentAvailability.AvailableTroops -= troopsQuantity;
            territory.TroopCount += troopsQuantity;
        }

        public void DistributeFreeTroops(Territory territory, int troopsQuantity)
        {
            if (AvailableFreeDistributionTroops < troopsQuantity)
            {
                throw new NotEnoughTroopsException();
            }

            if (territory.GetOccupant() != this)
            {
                throw new TerritoryNotOccupiedByPlayerException();
            }

            AvailableFreeDistributionTroops -= troopsQuantity;
            territory.TroopCount += troopsQuantity;
        }
    }
}
