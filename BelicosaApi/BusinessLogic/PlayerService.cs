using BelicosaApi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi.BusinessLogic
{
    public class PlayerService : ContextServiceBase<BelicosaApiContext>
    {
        public PlayerService(BelicosaApiContext context) : base(context) { }

        public async Task<Player?> Get(int playerId)
        {
            return await _context.Player.FindAsync(playerId);
        }

        public async Task<Player?> GetUserAsPlayer(BelicosaGame game, IdentityUser user)
        {
            return await _context.Player.Where(p => p.UserId == user.Id && p.GameId == game.Id).SingleOrDefaultAsync();
        }
    }
}
