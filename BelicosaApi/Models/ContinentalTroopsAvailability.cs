namespace BelicosaApi.Models
{
    public class ContinentalTroopsAvailability
    {
        public int Id { get; set; }
        public Player Player {  get; set; }
        public Continent Continent {  get; set; }
        public int AvailableTroops { get; set; }
    }
}
