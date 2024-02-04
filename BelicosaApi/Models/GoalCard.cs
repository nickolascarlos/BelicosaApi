namespace BelicosaApi.Models
{
    public class GoalCard
    {
        public int Id { get; set; }
        public BelicosaGame Game { get; set; } = null!;
        public string Description { get; set; } = string.Empty;
    }
}
