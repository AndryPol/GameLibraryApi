using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GameLibraryApi.Data
{
    public class GameLibraryApiContext : DbContext
    {
        public GameLibraryApiContext(DbContextOptions<GameLibraryApiContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder
           .Entity<Game>()
           .HasMany(p => p.Genres)
           .WithMany(p => p.Games)
          .UsingEntity(j => j.ToTable("GameGenres"));
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Genre>().ToTable("Genre");
        }

        public DbSet<GameLibraryApi.Data.Game> Games { get; set; }
        public DbSet<GameLibraryApi.Data.Genre> Genres { get; set; }
    }
}
