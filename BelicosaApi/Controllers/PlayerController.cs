using AutoMapper;
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
        private BelicosaApiContext BelicosaContext {  get; set; }
        private IMapper Mapper { get; set; }

        public PlayerController(BelicosaApiContext context, IMapper mapper)
        {
            BelicosaContext = context;
            Mapper = mapper;
        }

        [HttpGet("{id}")]
        public ActionResult GetPlayer(int id)
        {
            Player? player = BelicosaContext.Players.Include(p => p.User).SingleOrDefault(p => p.Id == id);

            if (player is null)
            {
                return NotFound();
            }

            return Ok(Mapper.Map<GetPlayerDTO>(player));
        }
    }
}
