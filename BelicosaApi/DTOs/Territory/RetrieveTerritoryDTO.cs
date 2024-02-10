using AutoMapper.Configuration.Annotations;
using BelicosaApi.DTOs.Player;
using BelicosaApi.Models;

namespace BelicosaApi.DTOs.Territory
{
    public class RetrieveTerritoryDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public string Name { get; set; } = null!;
        public RetrievePlayerDTO? OccupyingPlayer { get; set; }
        public List<RetrieveTerritory_TerritoryTerritoryDTO> BordersWith { get; set; } = new();
        public int TroopCount { get; set; } = 0;
    }
}
