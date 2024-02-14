using AutoMapper;
using BelicosaApi.BusinessLogic;
using BelicosaApi.DTOs.TerritoryCard;
using BelicosaApi.Exceptions;
using BelicosaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BelicosaApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryCardController : ControllerBase
    {
        private readonly TerritoryCardService _territoryCardService;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly PlayerService _playerService;
        private readonly IMapper _mapper;

        public TerritoryCardController(
            TerritoryCardService territoryCardService,
            PlayerService playerService,
            UserManager<IdentityUser> userManager,
            IMapper mapper
            )
        {
            _territoryCardService = territoryCardService;
            _userManager = userManager;
            _playerService = playerService;
            _mapper = mapper;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> Get(int id)
        {
            TerritoryCard? card = await _territoryCardService.Get(id);

            if (card is null)
            {
                return Problem("Territory card not found", statusCode: StatusCodes.Status404NotFound);
            }

            RetrieveTerritoryCardDTO returnableCard = _mapper.Map<RetrieveTerritoryCardDTO>(card);

            return Ok(returnableCard);
        }


        [Authorize]
        [HttpPost("exchange")]
        public async Task<ActionResult> Exchange(ExchangeCardsDTO exchangeCards)
        {
            List<TerritoryCard?> cards = new();

            foreach (int cardId in exchangeCards.CardsIds)
            {
                cards.Add(await _territoryCardService.Get(cardId));
            }

            if (cards.Any(card => card is null))
            {
                return Problem("Not all cards were found", statusCode: StatusCodes.Status404NotFound);
            }

            IdentityUser? currentUser = await _userManager.GetUserAsync(User);

            if (currentUser is null)
            {
                return Problem("Invalid user", statusCode: StatusCodes.Status500InternalServerError);
            }

            BelicosaGame? game = cards.FirstOrDefault()?.Game;

            if (game is null)
            {
                return Problem("Could not find game associated with the cards", statusCode: StatusCodes.Status500InternalServerError);
            }

            Player? player = await _playerService.GetUserAsPlayer(game, currentUser);

            if (player is null)
            {
                return Problem("Could not find player associated with the game", statusCode: StatusCodes.Status500InternalServerError);
            }


            try
            {
                await _territoryCardService.ExchangeCards(game, cards!, player);
                return Ok();
            }
            catch (CardNotOwnedByPlayerException)
            {
                return Problem("Not all cards belong to player", statusCode: StatusCodes.Status403Forbidden);
            }
            catch (CardNotBelongingToGameException)
            {
                return Problem("Not all cards belong to the same game", statusCode: StatusCodes.Status403Forbidden);
            }
            catch (ArgumentException e)
            {
                return Problem(e.Message, statusCode: StatusCodes.Status400BadRequest);
            }
        }
    }

}
