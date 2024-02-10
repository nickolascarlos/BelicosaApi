﻿using BelicosaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi.BusinessLogic
{
    public class TerritoryService
    {
        private readonly BelicosaApiContext _context;

        public TerritoryService(BelicosaApiContext context)
        {
            _context = context;
        }

        public async Task<Territory?> Get(int territoryId)
        {
            Territory? territory = await _context.Territory
                .Include(t => t.CanAttack)
                .Include(t => t.MayBeAttackedBy)
                .SingleOrDefaultAsync(territory => territory.Id == territoryId);
            
            if (territory is not null)
            {
                foreach (var jk in territory.CanAttack)
                {
                    Console.WriteLine(jk.ToString());
                }
            }

            return territory;
        }

        public async Task<List<Territory>> GetAll(BelicosaGame game)
        {
            return await _context.Territory
                .Include(t => t.CanAttack)
                .Include(t => t.MayBeAttackedBy)
                .Where(t => t.Game.Id == game.Id).ToListAsync();
        }

        public async Task<Territory> Create(string name, Continent continent, BelicosaGame game)
        {
            Territory territory = new Territory
            {
                Name = name,
                TroopCount = 0,
                Game = game
            };

            continent.Territories.Add(territory);
            
            await _context.SaveChangesAsync();

            return territory;
        }

    }
}