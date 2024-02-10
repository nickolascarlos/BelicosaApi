using BelicosaApi.DTOs.Player;
using BelicosaApi.DTOs.Territory;
using BelicosaApi.Models;

namespace BelicosaApi.DTOs.TerritoryCard
{
    public class RetrievePlayerTerritoryCardDTO
    {
        public int Id { get; set; }
        public string TerritoryName { get; set; }
        public Shape Shape { get; set; }
    }
}
