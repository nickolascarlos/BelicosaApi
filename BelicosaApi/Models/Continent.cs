namespace BelicosaApi.Models
{
    public class Continent
    {
        public int Id { get; set; }
        public BelicosaGame Game { get; set; } = null!;
        public string Name { get; set; } = null!;
        public List<Territory> Territories { get; set; } = null!;
    }
}
