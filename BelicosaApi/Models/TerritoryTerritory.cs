using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BelicosaApi.Models
{
    public class TerritoryTerritory
    {
        public int Id { get; set; }
        public int TerritoryId { get; set; }
        public int TerritoryToId { get; set; }

        [JsonIgnore]
        public Territory Territory { get; set; }

        [JsonIgnore]
        public Territory TerritoryTo { get; set; }
    }
}
