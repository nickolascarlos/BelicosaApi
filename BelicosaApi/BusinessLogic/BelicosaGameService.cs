using AutoMapper;
using BelicosaApi.BusinessLogic;
using BelicosaApi.DTOs.Game;
using BelicosaApi.Exceptions;
using BelicosaApi.Migrations;
using BelicosaApi.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BelicosaApi.ModelsServices
{
    public class BelicosaGameService : ContextServiceBase<BelicosaApiContext>
    {
        private readonly IAuthorizationService _authService;
        private readonly ContinentService _continentService;
        private readonly TerritoryService _territoryService;

        public BelicosaGameService(
            BelicosaApiContext context,
            IAuthorizationService authService,
            ContinentService continentService,
            TerritoryService territoryService
            ) : base(context)
        {
            _authService = authService;
            _continentService = continentService;
            _territoryService = territoryService;
        }

        public async Task<BelicosaGame?> Get(int id)
        {
            return await _context.Game.Include(game => game.Owner)
                                       .Include(game => game.Players)
                                       .FirstOrDefaultAsync(game => game.Id == id);
        }

        public async Task<BelicosaGame> Create(CreateGameDTO game, IdentityUser owner)
        {
            BelicosaGame newGame = new BelicosaGame
            {
                Title = game.Title,
                Owner = owner
            };

            Player player = new Player
            {
                Game = newGame,
                User = owner,
                ArmyColor = newGame.GetRandomAvailableColor(),
            };

            _context.Add(newGame);
            _context.Add(player);
            await _context.SaveChangesAsync();

            return newGame;
        }

        public async Task<Player> AddPlayer(BelicosaGame game, IdentityUser user)
        {
            if (game is null || user is null)
            {
                throw new ArgumentNullException();
            }

            if (game.ReachedMaximumNumberOfPlayers())
            {
                throw new MaximumNumberOfPlayersReachedException();
            }

            Color randomAvailableColor = game.GetRandomAvailableColor();

            Player newPlayer = new Player
            {
                Game = game,
                User = user,
                ArmyColor = randomAvailableColor
            };

            _context.Add(newPlayer);
            await _context.SaveChangesAsync();

            return newPlayer;
        }

        public async Task Start(BelicosaGame game)
        {
            game.Status = GameStatus.Started;
            game.StartTime = DateTime.Now.ToUniversalTime();
            _context.Game.Update(game);
            await _context.SaveChangesAsync();

            await Initialize(game);
        }

        // TODO: Decouple this
        private async Task Initialize(BelicosaGame game)
        {
            Continent africa = await _continentService.Create("África", game);
            Continent europe = await _continentService.Create("Europa", game);

            Territory argelia = await _territoryService.Create("Argélia", africa, game);
            Territory nigeria = await _territoryService.Create("Nigéria", africa, game);
            Territory france = await _territoryService.Create("França", europe, game);
            Territory germany = await _territoryService.Create("Alemanha", europe, game);
            Territory england = await _territoryService.Create("Inglaterra", europe, game);

            List<Territory> territories = new List<Territory>() { argelia, nigeria, france, germany, england };

            argelia.AddBorder(nigeria);
            germany.AddBorder(france);
            nigeria.AddBorder(argelia);
            nigeria.AddBorder(france);
            france.AddBorder(nigeria);
            france.AddBorder(england);
            france.AddBorder(germany);
            england.AddBorder(france);

            _context.UpdateRange([argelia, nigeria, france, germany, england]);

            await _context.SaveChangesAsync();

            await DistributeTerritories(game, territories);
        }

        private async Task DistributeTerritories(BelicosaGame game, List<Territory> territories)
        {
            List<Player> players = game.Players;

            foreach (Territory territory in territories)
            {
                territory.OccupyingPlayer = players[new Random().Next(0, players.Count())];
                _context.Update(territory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
