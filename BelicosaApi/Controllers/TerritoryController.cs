using BelicosaApi.BusinessLogic;
using BelicosaApi.Exceptions;
using BelicosaApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BelicosaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : ControllerBase
    {
        private readonly TerritoryService _territoryService;
        private readonly PlayerService _playerService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public TerritoryController(
            TerritoryService territoryService,
            PlayerService playerService,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager
            )
        {
            _territoryService = territoryService;
            _playerService = playerService;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetTerritory(int id)
        {
            Territory? territory = await _territoryService.Get(id);

            if (territory is null)
            {
                return Problem("Territory not found", statusCode: StatusCodes.Status404NotFound);
            }

            return Ok(territory);
        }

        [Authorize]
        [HttpPost("{territoryId}/placeFreeTroops/{troopsCount}")]
        public async Task<ActionResult> PlaceFreeTroops(int territoryId, int troopsCount)
        {
            IdentityUser? currentUser = await _userManager.GetUserAsync(User);

            if (currentUser is null)
            {
                return Problem("Invalid user", statusCode: StatusCodes.Status404NotFound);
            }

            Territory? territory = await _territoryService.Get(territoryId);

            if (territory is null)
            {
                return Problem("Territory not found", statusCode: StatusCodes.Status404NotFound);
            }

            BelicosaGame? game = await _territoryService.GetGameFromTerritory(territoryId);
            
            if (game is null)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            Player? player = await _playerService.GetUserAsPlayer(game, currentUser);

            if (player is null)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            try
            {
                await _territoryService.PlaceFreeTroops(territory, troopsCount, player);
            }
            catch (NotEnoughTroopsException)
            {
                return Problem("Not enough troops", statusCode: StatusCodes.Status403Forbidden);
            }
            catch (TerritoryNotOccupiedByPlayerException)
            {
                return Problem("Territory does not belong to player", statusCode: StatusCodes.Status403Forbidden);
            }

            return Ok();
        }
        [Authorize]
        [HttpPost("{fromId}/move/{troopsCount}/TroopsTo/{toId}")]
        public async Task<ActionResult> MoveTroopsBetweenTerritories(int fromId, int toId, int troopsCount)
        {
            IdentityUser? currentUser = await _userManager.GetUserAsync(User);

            if (currentUser is null)
            {
                return Problem("Invalid user", statusCode: StatusCodes.Status404NotFound);
            }

            Territory? from = await _territoryService.Get(fromId);
            Territory? to = await _territoryService.Get(toId);

            if (from is null || to is null)
            {
                return Problem("At least one territory was not found", statusCode: StatusCodes.Status404NotFound);
            }

            BelicosaGame? game = await _territoryService.GetGameFromTerritory(from);

            if (game is null)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            Player? player = await _playerService.GetUserAsPlayer(game, currentUser);

            if (player is null)
            {
                return Problem(statusCode: StatusCodes.Status500InternalServerError);
            }

            try
            {
                await _territoryService.MoveTroops(from, to, troopsCount, player);
                return Ok();
            }
            catch (NotEnoughTroopsException)
            {
                return Problem("Not enough troops", statusCode: StatusCodes.Status403Forbidden);
            }
            catch (TerritoryNotOccupiedByPlayerException)
            {
                return Problem("Territory is not occupied by player", statusCode: StatusCodes.Status403Forbidden);
            }
            catch (NonAdjacentTerritoriesException)
            {
                return Problem("Territories don't border", statusCode: StatusCodes.Status403Forbidden);
            }
        }
    
    }
}
