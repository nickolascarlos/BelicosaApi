using BelicosaApi.Exceptions;
using BelicosaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi.BusinessLogic
{
    public class TerritoryService
    {
        private readonly BelicosaApiContext _context;

        public TerritoryService(BelicosaApiContext context)
        {
            _context = context;
        }

        public async Task<Territory?> Get(int territoryId)
        {
            Territory? territory = await _context.Territory
                .Include(t => t.CanAttack)
                .Include(t => t.MayBeAttackedBy)
                .Include(t => t.OccupyingPlayer)
                .SingleOrDefaultAsync(territory => territory.Id == territoryId);
            
            return territory;
        }

        public async Task<List<Territory>> GetAll(BelicosaGame game)
        {
            return await _context.Territory
                .Include (t => t.OccupyingPlayer)
                    .ThenInclude(t => t!.User)
                .Include(t => t.CanAttack)
                .Include(t => t.MayBeAttackedBy)
                .Where(t => t.GameId == game.Id)
                .ToListAsync();
        }

        public async Task<Territory> Create(string name, Continent continent, BelicosaGame game)
        {
            Territory territory = new Territory
            {
                Name = name,
                TroopCount = 0,
                Game = game,
                OccupyingPlayer = null
            };

            continent.Territories.Add(territory);
            
            await _context.SaveChangesAsync();

            return territory;
        }

        public async Task<List<Territory>> GetFromPlayer(int playerId)
        {
            return await _context.Territory
                .Include(territory => territory.CanAttack)
                    .ThenInclude(territoryTerritory => territoryTerritory.TerritoryTo)
                .Where(territory => (territory.OccupyingPlayer != null) && territory.OccupyingPlayer!.Id == playerId)
                .ToListAsync();
        }

        public async Task<List<Territory>> GetFromPlayer(Player player)
        {
            return await GetFromPlayer(player.Id);
        }

        public async Task PlaceFreeTroops(Territory territory, int troopsToAdd, Player player)
        {
            if (territory.OccupyingPlayerId != player.Id)
            {
                throw new TerritoryNotOccupiedByPlayerException();
            }

            if (player.AvailableFreeDistributionTroops < troopsToAdd)
            {
                throw new NotEnoughTroopsException();
            }

            player.AvailableFreeDistributionTroops -= troopsToAdd;
            _context.Update(player);

            territory.TroopCount += troopsToAdd;
            _context.Update(territory);

            await _context.SaveChangesAsync();
        }

        public async Task<BelicosaGame?> GetGameFromTerritory(int territoryId)
        {
            return (await _context.Territory
                .Include(territory => territory.Game)
                .Where(territory => territory.Id == territoryId)
                .SingleOrDefaultAsync())
                !.Game;
        }

        public async Task<BelicosaGame?> GetGameFromTerritory(Territory territory)
        {
            return await GetGameFromTerritory(territory.Id);
        }

        public async Task MoveTroops(Territory from, Territory to, int movingTroopCount, Player player)
        {
            if (from.OccupyingPlayerId != player.Id || to.OccupyingPlayerId != player.Id)
            {
                throw new TerritoryNotOccupiedByPlayerException();
            }

            // There must always be at least 1 troop in the occupied territory
            // That's why it is not allowed to move all the troops
            if (from.TroopCount <= movingTroopCount)
            {
                throw new NotEnoughTroopsException();
            }

            if (!BordersWith(from, to))
            {
                throw new NonAdjacentTerritoriesException();
            }

            from.TroopCount -= movingTroopCount;
            to.TroopCount += movingTroopCount;

            _context.UpdateRange([from, to]);
            await _context.SaveChangesAsync();
        }

        public bool BordersWith(Territory territory1, Territory territory2)
        {
            return territory1.CanAttack.Any(t => t.Id == territory2.Id);
        }

    }
}
