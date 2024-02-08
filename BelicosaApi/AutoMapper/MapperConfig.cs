using AutoMapper;
using BelicosaApi.DTOs.Game;
using BelicosaApi.DTOs.Player;
using BelicosaApi.Models;

namespace BelicosaApi.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<BelicosaGame, RetrieveGameDTO>();
            CreateMap<Player, RetrieveGamePlayerDTO>();
            CreateMap<Player, GetPlayerDTO>();
        }

    }
}
