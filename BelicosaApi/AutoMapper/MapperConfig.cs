using AutoMapper;
using BelicosaApi.DTOs.Game;
using BelicosaApi.DTOs.Player;
using BelicosaApi.DTOs.Territory;
using BelicosaApi.DTOs.TerritoryCard;
using BelicosaApi.DTOs.User;
using BelicosaApi.Models;
using Microsoft.AspNetCore.Identity;

namespace BelicosaApi.AutoMapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig() 
        {
            CreateMap<BelicosaGame, RetrieveGameDTO>();
            CreateMap<Player, RetrieveGamePlayerDTO>();
            CreateMap<Player, RetrievePlayerDTO>();
            CreateMap<Territory, RetrieveTerritoryDTO>()
                .ForMember("BordersWith", x => x.MapFrom(territory => territory.CanAttack));
            CreateMap<Territory, TerritoryAsBorderDTO>();
            CreateMap<TerritoryTerritory, RetrieveTerritory_TerritoryTerritoryDTO>()
                .ForMember("Id", x => x.MapFrom(territoryTerritory => territoryTerritory.TerritoryTo.Id))
                .ForMember("Name", x => x.MapFrom(territoryTerritory => territoryTerritory.TerritoryTo.Name));
            CreateMap<TerritoryCard, RetrieveTerritoryCardDTO>();
            CreateMap<IdentityUser, RetrieveUserDTO>();
            CreateMap<Player, NoGameId_RetrievePlayerDTO>();


        }

    }
}
