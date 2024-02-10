using AutoMapper;
using BelicosaApi.BusinessLogic;
using BelicosaApi.DTOs.TerritoryCard;
using BelicosaApi.Models;
using Microsoft.AspNetCore.Mvc;

namespace BelicosaApi.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class TerritoryCardController : ControllerBase
    {
        private readonly TerritoryCardService _territoryCardService;
        private readonly IMapper _mapper;

        public TerritoryCardController(TerritoryCardService territoryCardService, IMapper mapper)
        {
            _territoryCardService = territoryCardService;
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
    }
}
