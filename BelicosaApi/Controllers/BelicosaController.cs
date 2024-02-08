using AutoMapper;
using BelicosaApi.DTOs;
using BelicosaApi.DTOs.Game;
using BelicosaApi.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimplePatch;
using System.Net;
using System.Reflection;

namespace BelicosaApi.Controllers
{
    [Authorize]
    [Route("api/game")]
    [ApiController]
    public class BelicosaController : ControllerBase
    {
        private readonly BelicosaApiContext _belicosaContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;

        public BelicosaController(
            BelicosaApiContext context,
            IMapper mapper,
            IAuthorizationService authorizationService,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager
           )
        {
            _belicosaContext = context;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBelicosaGame([FromBody] CreateGameDTO game)
        {
            IdentityUser? owner = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            if (owner is null)
            {
                return Problem("Owner is not a valid user", statusCode: StatusCodes.Status404NotFound);
            }

            BelicosaGame belicosaGame = new BelicosaGame
            {
                 Title = game.Title,
                 Owner = owner
            };

            var chosenColor = belicosaGame.GetRandomAvailableColor();

            Player ownerAsPlayer = new Player
            {
                Game = belicosaGame,
                User = owner,
                ArmyColor = chosenColor
            };

            _belicosaContext.Add(belicosaGame);
            _belicosaContext.Add(ownerAsPlayer);
            _belicosaContext.SaveChanges();

            var returnBelicosaGame = _mapper.Map<RetrieveGameDTO>(belicosaGame);

            return CreatedAtAction(nameof(GetBelicosaGame), new { id = belicosaGame.Id }, returnBelicosaGame);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public ActionResult GetBelicosaGame(int id)
        {

            BelicosaGame? belicosaGame = _belicosaContext.Games
                                            .Include(game => game.Owner)
                                            .Include(game => game.Players)
                                            .FirstOrDefault(game => game.Id == id);
            
            if (belicosaGame is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RetrieveGameDTO>(belicosaGame));
        }

        [HttpPost("{id}/start")]
        public ActionResult StartBelicosaGame(int id)
        {
            BelicosaGame? belicosaGame = _belicosaContext.Games.Find(id);

            if (belicosaGame is null)
            {
                return NotFound();
            }

            belicosaGame.StartTime = DateTime.Now.ToUniversalTime();
            belicosaGame.Status = GameStatus.Started;
            _belicosaContext.SaveChanges();

            return Ok();
        }

        [HttpPost("{id}/addPlayer/{userId}")]
        public ActionResult AddPlayerToBelicosaGame(int id, int userId)
        {
            BelicosaGame? belicosaGame = _belicosaContext.Games.Find(id);
            
            if (belicosaGame is null) 
            {
                return Problem("Game not found", statusCode: StatusCodes.Status404NotFound);
            }

            IdentityUser? user = _belicosaContext.Users.Find(userId);

            if (user is null)
            {
                return Problem("User not found", statusCode: StatusCodes.Status404NotFound);
            }

            if (belicosaGame.AvailableColors.Count == 0)
            {
                return Problem("Maximum player capacity reached", statusCode: StatusCodes.Status403Forbidden);
            }

            var newPlayerColor = belicosaGame.GetRandomAvailableColor();
            
            Player player = new Player()
            {
                User = user,
                Game = belicosaGame,
                ArmyColor = newPlayerColor
            };


            _belicosaContext.Add(player);
            belicosaGame.Players.Add(player);

            try
            {
                _belicosaContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                throw;
            }

            return Created($"api/player/{player.Id}", player);
        }
    }
}
