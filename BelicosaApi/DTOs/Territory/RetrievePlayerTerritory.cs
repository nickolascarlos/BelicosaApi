using BelicosaApi.DTOs.Player;
using BelicosaApi.DTOs.Territory;

public class RetrievePlayerTerritoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<RetrieveTerritory_TerritoryTerritoryDTO> BordersWith { get; set; } = new();
    public int TroopCount { get; set; } = 0;
}