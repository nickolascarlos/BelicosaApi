using BelicosaApi.Models;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi
{
    public class BelicosaApiContext : DbContext
    {
        public DbSet<BelicosaGame> Games { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Territory> Territories { get; set; }




        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("Host=localhost;Database=MyDB;Username=postgres;Password=root");
            }
        }
    }
}
