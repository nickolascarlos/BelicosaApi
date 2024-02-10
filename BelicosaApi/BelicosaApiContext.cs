using BelicosaApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection.Emit;

namespace BelicosaApi
{
    public class BelicosaApiContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<BelicosaGame> Game { get; set; }
        // public DbSet<User> Users { get; set; }
        public DbSet<Player> Player { get; set; }
        public DbSet<Territory> Territory { get; set; }
        public DbSet<Continent> Continent { get; set; }
        public DbSet<TerritoryCard> TerritoryCard { get; set; }


        private readonly IConfiguration _config;

        public BelicosaApiContext(DbContextOptions<BelicosaApiContext> options, IConfiguration config) : base(options)
        {
            _config = config;
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<TerritoryTerritory>()
                .HasKey(t => t.Id);

            builder.Entity<TerritoryTerritory>()
                .HasOne(tt => tt.Territory)
                .WithMany(tf => tf.CanAttack)
                .HasForeignKey(x => x.TerritoryId);

            builder.Entity<TerritoryTerritory>()
                .HasOne(tt => tt.TerritoryTo)
                .WithMany(tf => tf.MayBeAttackedBy)
                .HasForeignKey(x => x.TerritoryToId);

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql(_config.GetSection("ConnectionString").Value);
                optionsBuilder.EnableSensitiveDataLogging();
            }
        }
    }
}
