using BelicosaApi.DTOs.Player;
using BelicosaApi.DTOs.Territory;
using BelicosaApi.Models;

namespace BelicosaApi.DTOs.TerritoryCard
{
    public class RetrieveTerritoryCardDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public RetrieveTerritoryDTO Territory { get; set; }
        public Shape Shape { get; set; }
        public NoGameId_RetrievePlayerDTO? Holder { get; set; }
    }
}
