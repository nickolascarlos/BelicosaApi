using AutoMapper;
using BelicosaApi.BusinessLogic;
using BelicosaApi.DTOs.Player;
using BelicosaApi.DTOs.Territory;
using BelicosaApi.DTOs.TerritoryCard;
using BelicosaApi.Enums;
using BelicosaApi.Models;
using BelicosaApi.ModelsServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi.Controllers
{
    [Route("api/player")]
    [ApiController]
    public class PlayerController : ControllerBase
    {
        private readonly PlayerService _playerService;
        private readonly TerritoryCardService _territoryCardService;
        private readonly TerritoryService _territoryService;
        private readonly BelicosaGameService _gameService;
        private readonly IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public PlayerController(
            PlayerService playerService,
            TerritoryCardService territoryCardService,
            TerritoryService territoryService,
            BelicosaGameService gameService,
            IAuthorizationService authorizationService,
            IMapper mapper
           )
        {
            _mapper = mapper;
            _playerService = playerService;
            _territoryCardService = territoryCardService;
            _territoryService = territoryService;
            _gameService = gameService;
            _authorizationService = authorizationService;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetPlayer(int id)
        {
            Player? player = await _playerService.Get(id);

            if (player is null)
            {
                return Problem("Player not found", statusCode: StatusCodes.Status404NotFound);
            }

            var returnablePlayer = _mapper.Map<RetrievePlayerDTO>(player);

            return Ok(returnablePlayer);
        }

        [Authorize]
        [HttpGet("{playerId}/territoryCards")]
        public async Task<ActionResult> GetTerritoryCards(int playerId)
        {
            Player? player = await _playerService.Get(playerId);
            
            if (player is null)
            {
                return Problem("Player not found", statusCode: StatusCodes.Status404NotFound);
            }

            AuthorizationResult authorization = await _authorizationService.AuthorizeAsync(User, player, CustomPolicies.UserIsPlayer);

            if (!authorization.Succeeded)
            {
                return Problem(String.Join("\n", authorization.Failure.FailureReasons.Select(reason => reason.Message)), statusCode: StatusCodes.Status403Forbidden);
            }

            List<TerritoryCard> cards = await _territoryCardService.GetFromPlayer(playerId);
            List<RetrievePlayerTerritoryCardDTO> returnableCards = _mapper.Map<List<RetrievePlayerTerritoryCardDTO>>(cards);

            return Ok(returnableCards);
        }

        [HttpGet("{playerId}/territories")]
        public async Task<ActionResult> GetTerritories(int playerId)
        {
            Player? player = await _playerService.Get(playerId);

            if (player is null)
            {
                return Problem("Player not found", statusCode: StatusCodes.Status404NotFound);
            }

            BelicosaGame? game = await _gameService.Get(player.GameId);

            if (game is null)
            {
                return Problem("Game not found", statusCode: StatusCodes.Status404NotFound);
            }

            List<Territory> territories = await _territoryService.GetFromPlayer(player.Id);

            List<RetrievePlayerTerritoryDTO> returnableTerritories = _mapper.Map<List<RetrievePlayerTerritoryDTO>>(territories);

            return Ok(returnableTerritories);
        }
    }
}
