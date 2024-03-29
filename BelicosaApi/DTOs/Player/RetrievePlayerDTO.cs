﻿using BelicosaApi.DTOs.User;
using BelicosaApi.Models;

namespace BelicosaApi.DTOs.Player
{
    public class RetrievePlayerDTO
    {
        public int Id { get; set; }
        public int GameId { get; set; }
        public RetrieveUserDTO User { get; set; } = null!;
        public Color ArmyColor { get; set; }
        public int AvailableFreeDistributionTroops { get; set; }
    }
}
