using BelicosaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi.BusinessLogic
{
    public class ContinentService : ContextServiceBase<BelicosaApiContext>
    {
        public ContinentService(BelicosaApiContext context) : base(context) { }

        public async Task<Continent?> Get(int continentId)
        {
            return await _context.Continents.SingleOrDefaultAsync(x => x.Id == continentId);
        }
        public async Task<List<Continent>> GetAll(BelicosaGame game)
        {
            return await _context.Continents.Where(c => c.Game.Id == game.Id).ToListAsync();
        }
        
        public async Task<Continent> Create(string name, BelicosaGame game)
        {
            Continent continent = new Continent
            {
                Name = name,
                Game = game
            };

            _context.Add(continent);
            await _context.SaveChangesAsync();

            return continent;
        }
    }
}
