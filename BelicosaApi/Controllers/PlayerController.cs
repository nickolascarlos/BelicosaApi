using AutoMapper;
using BelicosaApi.BusinessLogic;
using BelicosaApi.DTOs.Player;
using BelicosaApi.Models;
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
        private readonly IMapper _mapper;

        public PlayerController(PlayerService playerService, IMapper mapper)
        {
            _playerService = playerService;
            _mapper = mapper;
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
    }
}
