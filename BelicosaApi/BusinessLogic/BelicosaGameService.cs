using AutoMapper;
using BelicosaApi.DTOs.Game;
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
    public class BelicosaGameService
    {
        private readonly BelicosaApiContext _context;
        private readonly IAuthorizationService _authService;

        public BelicosaGameService(
            BelicosaApiContext context,
            IAuthorizationService authService
            )
        {
            _context = context;
            _authService = authService;
        }

        public async Task<BelicosaGame?> Get(int id)
        {
            return await _context.Games.Include(game => game.Owner)
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
            _context.Games.Update(game);
            await _context.SaveChangesAsync();
        }
    }
}
