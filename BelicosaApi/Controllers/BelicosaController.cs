﻿using AutoMapper;
using BelicosaApi.DTOs;
using BelicosaApi.DTOs.Game;
using BelicosaApi.DTOs.Player;
using BelicosaApi.Models;
using BelicosaApi.ModelsServices;
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
        // private readonly BelicosaApiContext _belicosaContext;
        private readonly IMapper _mapper;
        private readonly IAuthorizationService _authorizationService;
        private readonly Microsoft.AspNetCore.Identity.UserManager<IdentityUser> _userManager;
        private readonly BelicosaGameService _gameService;

        public BelicosaController(
            BelicosaApiContext context,
            IMapper mapper,
            IAuthorizationService authorizationService,
            Microsoft.AspNetCore.Identity.UserManager<IdentityUser> userManager,
            BelicosaGameService belicosaGameService
           )
        {
            //_belicosaContext = context;
            _mapper = mapper;
            _authorizationService = authorizationService;
            _userManager = userManager;
            _gameService = belicosaGameService;
        }

        [HttpPost]
        public async Task<ActionResult> CreateBelicosaGame([FromBody] CreateGameDTO game)
        {
            IdentityUser? owner = await _userManager.FindByIdAsync(User.Identity.GetUserId());

            if (owner is null)
            {
                return Problem("Owner is not a valid user", statusCode: StatusCodes.Status404NotFound);
            }

            BelicosaGame createdGame = await _gameService.Create(game, owner);

            var returnableBelicosaGame = _mapper.Map<RetrieveGameDTO>(createdGame);

            return CreatedAtAction(nameof(GetBelicosaGame), new { id = createdGame.Id }, returnableBelicosaGame);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public async Task<ActionResult> GetBelicosaGame(int id)
        {
            BelicosaGame? belicosaGame = await _gameService.Get(id);
            
            if (belicosaGame is null)
            {
                return NotFound();
            }

            var returnableBelicosaGame = _mapper.Map<RetrieveGameDTO>(belicosaGame);

            return Ok(returnableBelicosaGame);
        }

        [HttpPost("{id}/start")]
        public async Task<ActionResult> StartBelicosaGame(int id)
        {
            BelicosaGame? belicosaGame = await _gameService.Get(id);

            if (belicosaGame is null)
            {
                return NotFound();
            }

            var result = await _authorizationService.AuthorizeAsync(User, belicosaGame, "UserIsGameOwner");

            if (!result.Succeeded)
            {
                return Problem("Only the owner may start the game", statusCode: StatusCodes.Status403Forbidden);
            }

            await _gameService.Start(belicosaGame);

            return Ok();
        }

        [HttpPost("{gameId}/addPlayer/{userId}")]
        public async Task<ActionResult> AddPlayerToBelicosaGame(int gameId, string userId)
        {
            BelicosaGame? game = await _gameService.Get(gameId);
            
            if (game is null) 
            {
                return Problem("Game not found", statusCode: StatusCodes.Status404NotFound);
            }

            IdentityUser? user = await _userManager.FindByIdAsync(userId);

            if (user is null)
            {
                return Problem("User not found", statusCode: StatusCodes.Status404NotFound);
            }

            // TODO: Should it be here?
            if (game.AvailableColors.Count == 0)
            {
                return Problem("Maximum player capacity reached", statusCode: StatusCodes.Status403Forbidden);
            }

            Player addedPlayer = await _gameService.AddPlayer(game, user);

            var returnableAddedPlayer = _mapper.Map<GetPlayerDTO>(addedPlayer); 

            // TODO: Replace hard-coded route
            return Created($"api/player/{addedPlayer.Id}", returnableAddedPlayer);
        }
    }
}
