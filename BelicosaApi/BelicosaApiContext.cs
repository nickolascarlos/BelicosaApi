using BelicosaApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BelicosaApi
{
    public class BelicosaApiContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<BelicosaGame> Games { get; set; }
        // public DbSet<User> Users { get; set; }
        public DbSet<Player> Players { get; set; }
        public DbSet<Territory> Territories { get; set; }


        private readonly IConfiguration _config;

        public BelicosaApiContext(DbContextOptions<BelicosaApiContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_config.GetSection("ConnectionString").Value);
            }
        }
    }
}
