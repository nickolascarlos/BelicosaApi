namespace BelicosaApi.DTOs
{
    public class BattleResultDTO
    {
        public int AttackerTroopsLoss { get; set; }
        public int DefenderTroopsLoss { get; set; }
        public bool Conquered { get; set; }
    }
}
