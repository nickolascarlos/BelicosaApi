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

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Player>()
        //        .HasOne(p => p.User)
        //        .WithOne()
        //        .IsRequired();
        //}

        public BelicosaApiContext(DbContextOptions<BelicosaApiContext> options) : base(options) { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseNpgsql("");
            }
        }
    }
}
