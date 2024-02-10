using BelicosaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi.BusinessLogic
{
    public class TerritoryCardService : ContextServiceBase<BelicosaApiContext>
    {
        public TerritoryCardService(BelicosaApiContext context) : base(context)
        {
        }

        public async Task<TerritoryCard?> Get(int territoryCardId)
        {
            return await _context.TerritoryCard
                .Include(card => card.Territory)
                    .ThenInclude(territory => territory.CanAttack)
                .Include(card => card.Holder)
                    .ThenInclude(player => player!.User)
                .FirstOrDefaultAsync(card => card.Id == territoryCardId);
        }

        public async Task<List<TerritoryCard>> GetAll(int gameId)
        {
            return _context.TerritoryCard
                .Include(card => card.Territory)
                    .ThenInclude(territory => territory.CanAttack)
                .Include(card => card.Holder)
                    .ThenInclude(player => player!.User)
                .Where(card => card.Game.Id == gameId)
                .ToList();
        }

        public async Task<List<TerritoryCard>> GetAll(BelicosaGame game)
        {
            return await GetAll(game.Id);
        }
    }
}
