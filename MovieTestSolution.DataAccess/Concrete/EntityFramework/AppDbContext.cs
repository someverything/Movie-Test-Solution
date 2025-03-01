using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MovieTestSolution.Core.Configuration;
using MovieTestSolution.Entities.Concrete;
using MovieTestSolution.Entities.Concrete.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace MovieTestSolution.DataAccess.Concrete.EntityFramework
{
    public class AppDbContext : IdentityDbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer("Server = localhost; Database = MovieSolutionDb; Trusted_Connection = True; TrustServerCertificate = True");
            optionsBuilder.UseSqlServer(DatabaseConfiguration.ConnectionString);
        }

        public DbSet<Actor> Actors { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<Director> Directors { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<MovieActor> MovieActors { get; set; }
        public DbSet<MovieCountry> MovieCountries { get; set; }
        public DbSet<MovieStudio> MovieStudios { get; set; }
        public DbSet<MovieGenre> MovieGenres { get; set; }
        public DbSet<Studio> Studios { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //MovieGenre config
            builder.Entity<MovieGenre>()
                .HasKey(mg => new { mg.MovieId, mg.GenreId });

            builder.Entity<MovieGenre>()
                .HasOne(mg => mg.Movie)
                .WithMany(m => m.MovieGenres)
                .HasForeignKey(mg => mg.MovieId);

            builder.Entity<MovieGenre>()
                .HasOne(m => m.Genre)
                .WithMany(g => g.MovieGenres)
                .HasForeignKey(mg => mg.GenreId);

            //MovieActor config
            builder.Entity<MovieActor>()
                .HasKey(ma => new { ma.MovieId, ma.ActorId });

            builder.Entity<MovieActor>()
                .HasOne(ma => ma.Movie)
                .WithMany(m => m.MovieActors)
                .HasForeignKey(ma => ma.MovieId);

            builder.Entity<MovieActor>()
                .HasOne(ma => ma.Actor)
                .WithMany(a => a.MovieActors)
                .HasForeignKey(ma => ma.ActorId);

            //MovieStudio Config
            builder.Entity<MovieStudio>()
                .HasKey(ms => new { ms.MovieId, ms.StudioId });

            builder.Entity<MovieStudio>()
                .HasOne(ms => ms.Movie)
                .WithMany(m => m.MovieStudios)
                .HasForeignKey(ms => ms.MovieId);

            builder.Entity<MovieStudio>()
                .HasOne(ms => ms.Studio)
                .WithMany(s => s.MovieStudios)
                .HasForeignKey(ms => ms.StudioId);

            //MovieContuntry config
            builder.Entity<MovieCountry>()
                .HasKey(mc => new { mc.MovieId, mc.CountryId });

            builder.Entity<MovieCountry>()
                .HasOne(mc => mc.Movie)
                .WithMany(m => m.MovieCountries)
                .HasForeignKey(mc => mc.MovieId);

            builder.Entity<MovieCountry>()
                .HasOne(mc => mc.Country)
                .WithMany(c => c.MovieCountries)
                .HasForeignKey(mc => mc.CountryId);

            //Movie config
            builder.Entity<Movie>()
                .HasOne(m => m.Director)
                .WithMany(d => d.Movies)
                .HasForeignKey(m => m.DirectorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Entity<Movie>()
                .Property(m => m.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Entity<Movie>()
                .Property(m => m.ReleaseDate)
                .IsRequired();

            builder.Entity<Movie>()
                .HasIndex(m => m.Title)
                .IsUnique();

            //Genre config
            builder.Entity<Genre>()
                .Property(g => g.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            //Studio config
            builder.Entity<Studio>()
                .Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(150);
            
            //Country config
            builder.Entity<Country>()
                .Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);
            
            //Actor config
            builder.Entity<Actor>()
                .Property(a => a.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Actor>()
                .Property(a => a.LastName)
                .IsRequired()
                .HasMaxLength(100);
            
            //Director config
            builder.Entity<Director>()
                .Property(d => d.FirstName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Entity<Director>()
                .Property(d => d.LastName)
                .IsRequired()
                .HasMaxLength(100);
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
