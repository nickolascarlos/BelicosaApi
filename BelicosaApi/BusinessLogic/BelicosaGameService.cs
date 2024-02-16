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
using System.IO.Compression;
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

            if (game.Players.Any(player => player.UserId == user.Id))
            {
                throw new UserAlreadyInGameException();
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

            await GivePlayerFreeTroops(game.Players[0]);
        }

        private async Task DistributeTerritories(BelicosaGame game, List<Territory> territories)
        {
            List<Player> players = game.Players;

            foreach (Territory territory in territories)
            {
                // --------------------
                TerritoryCard card = new TerritoryCard
                {
                    Game = game,
                    Territory = territory,
                    Holder = players[new Random().Next(0, players.Count())],
                    Shape = Shape.Triangle
                };
                _context.Add(card);
                // --------------------
                territory.OccupyingPlayer = players[new Random().Next(0, players.Count())];
                territory.TroopCount += 1;
                _context.Update(territory);
                await _context.SaveChangesAsync();
            }
        }

        private async Task GivePlayerFreeTroops(Player player)
        {
            List<Territory> playerTerritories = await _territoryService.GetFromPlayer(player);

            int givenTroopsCount = playerTerritories.Count();

            player.AvailableFreeDistributionTroops += givenTroopsCount;

            _context.Update(player);
            await _context.SaveChangesAsync();
        }

        public async Task<Tuple<int, int, bool>> Attack(Territory attacker, Territory attacked, Player player)
        {
            if (attacker.OccupyingPlayerId != player.Id)
            {
                throw new TerritoryNotOccupiedByPlayerException();
            }

            if (attacked.OccupyingPlayerId == player.Id)
            {
                throw new AttackingOwnTerritoryException();
            }

            if (attacker.TroopCount == 1)
            {
                throw new NotEnoughTroopsException();
            }

            int attackerTroopCount = int.Min(3, attacker.TroopCount-1);
            int defenderTroopCount = int.Min(3, attacked.TroopCount);

            List<int> attackerDice = ThrowDice(attackerTroopCount);
            List<int> defenderDice = ThrowDice(defenderTroopCount);

            var (attackWins, defendWins) = CalculateBattleResult(attackerDice, defenderDice);
            attacked.TroopCount -= attackWins;
            attacker.TroopCount -= defendWins;
            bool conquered = attacked.TroopCount == 0;

            if (conquered)
            {
                attacked.OccupyingPlayer = player;
                attacked.TroopCount += 1;
                attacker.TroopCount -= 1;
            }
            _context.UpdateRange([attacked, attacker]);
            await _context.SaveChangesAsync();

            return new Tuple<int, int, bool>(attackWins, defendWins, conquered);
        }

        private static List<int> ThrowDice(int throws)
        {
            return Enumerable.Range(0, throws).Select(_ => new Random().Next(1, 6)).ToList();
        }

        private static List<int> ThrowSortedDice(int throws)
        {
            return ThrowDice(throws).OrderDescending().ToList();
        }

        private static Tuple<int, int> CalculateBattleResult(List<int> attackerDice, List<int> attackedDice)
        {
            attackerDice = attackerDice.OrderDescending().ToList();
            attackedDice = attackedDice.OrderDescending().ToList();
            var zippedDice = attackerDice.Zip(attackedDice, (a, b) => new Tuple<int, int>(a, b));

            var (attackerWins, attackedWins) = (0, 0);
            foreach (var (attackerDie, attackedDie) in zippedDice)
            {
                if (attackerDie > attackedDie)
                {
                    attackerWins += 1;
                }
                else
                {
                    attackedWins += 1;
                }
            }

            return new Tuple<int, int>(attackerWins, attackedWins);
        }
    }
}
