using BelicosaApi.Models;
using Microsoft.AspNetCore.Identity;

namespace BelicosaApi.BusinessLogic
{
    public class PlayerService : ContextServiceBase<BelicosaApiContext>
    {
        public PlayerService(BelicosaApiContext context) : base(context) { }

        public async Task<Player?> Get(int playerId)
        {
            return await _context.Player.FindAsync(playerId);
        }
    }
}
