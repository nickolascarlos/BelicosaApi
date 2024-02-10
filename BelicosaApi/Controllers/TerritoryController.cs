using BelicosaApi.BusinessLogic;
using BelicosaApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BelicosaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryController : ControllerBase
    {
        private readonly TerritoryService _territoryService;


        public TerritoryController(TerritoryService territoryService)
        {
            _territoryService = territoryService;
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

        //[HttpPost("{fromId}/moveTroopsTo/{toId}")]
        //public async Task MoveTroopsBetweenTerritories(int fromId, int toId)
        //{
        //    Territory? from = _context.Territories.SingleOrDefault(territory => territory.Id == fromId);

        //}
    }
}
