using Microsoft.EntityFrameworkCore;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Server = localhost; Database = MovieSolutionDb; Trusted_Connection = True; TrustServerCertificate = True");
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieCountry> MovieCountries { get; set; }
        public DbSet<MovieStudio> MovieStudios { get; set; }
        public DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            modelBuilder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            modelBuilder.Entity<MovieCountry>()
                .HasKey(mc => new { mc.MovieId, mc.CountryId });

            modelBuilder.Entity<MovieCountry>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCountries)
                .HasForeignKey(mc => mc.MovieId);

            modelBuilder.Entity<MovieCountry>()
                .HasOne(mc => mc.Country)
                .WithMany()
                .HasForeignKey(mc => mc.CountryId);

            modelBuilder.Entity<MovieStudio>()
    .HasKey(ms => new { ms.MovieId, ms.StudioId });

            modelBuilder.Entity<MovieStudio>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieStudios)
                .HasForeignKey(ms => ms.MovieId);

            modelBuilder.Entity<MovieStudio>()
                .HasOne(ms => ms.Studio)
                .WithMany(s => s.MovieStudios)
                .HasForeignKey(ms => ms.StudioId);

            modelBuilder.Entity<Movie>()
                .HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<Movie>()
                .HasMany(m => m.Genres)
                .WithMany(g => g.Movies)
                .UsingEntity(j => j.ToTable("MovieGenres"));
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.Now;
                        break;
                    default:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChangesAsync(cancellationToken);
        }

        public override int SaveChanges()
        {
            var datas = ChangeTracker.Entries<BaseEntity>();

            foreach (var data in datas)
            {
                switch (data.State)
                {
                    case EntityState.Added:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                    case EntityState.Modified:
                        data.Entity.UpdatedDate = DateTime.Now;
                        break;
                    default:
                        data.Entity.CreatedDate = DateTime.Now;
                        break;
                }
            }

            return base.SaveChanges();
        }
    }
}
