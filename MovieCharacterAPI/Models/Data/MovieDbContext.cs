using Microsoft.EntityFrameworkCore;
using MovieCharacterAPI.Models.Domain;

namespace MovieCharacterAPI.Models.Data
{
	public class MovieDbContext : DbContext
	{
		public MovieDbContext(DbContextOptions options) : base(options)
		{
		}

		public DbSet<Character> Characters { get; set; }
		public DbSet<Movie> Movies { get; set; }
		public DbSet<Franchise> Franchises { get; set; }



		protected override void OnModelCreating(ModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Movie>().HasOne(m => m.Franchise).WithMany(f => f.Movies).OnDelete(DeleteBehavior.SetNull);
			modelBuilder.Entity<Character>().HasData(new Character { Id = 1, FullName = "Han Solo" });
			modelBuilder.Entity<Character>().HasData(new Character { Id = 2, FullName = "Jack Sparrow" });
			modelBuilder.Entity<Character>().HasData(new Character { Id = 3, FullName = "Shrek" });
			modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 1, Name = "LucasFilm" });
			modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 2, Name = "Walt Disney" });
			modelBuilder.Entity<Franchise>().HasData(new Franchise { Id = 3, Name = "DreamWorks" });
			modelBuilder.Entity<Movie>().HasData(new Movie { Id = 1, Title = "Star Wars: A New Hope", FranchiseId = 1 });
			modelBuilder.Entity<Movie>().HasData(new Movie { Id = 2, Title = "Pirates of the Caribbean", FranchiseId = 2 });
			modelBuilder.Entity<Movie>().HasData(new Movie { Id = 3, Title = "Shrek", FranchiseId = 3 });
			modelBuilder.Entity<Character>()
				.HasMany(c => c.Movies)
				.WithMany(m => m.Characters)
				.UsingEntity(cm => cm.HasData(new { CharactersId = 1, MoviesId = 1 }));
			modelBuilder.Entity<Character>()
				.HasMany(c => c.Movies)
				.WithMany(m => m.Characters)
				.UsingEntity(cm => cm.HasData(new { CharactersId = 2, MoviesId = 2 }));
			modelBuilder.Entity<Character>()
				.HasMany(c => c.Movies)
				.WithMany(m => m.Characters)
				.UsingEntity(cm => cm.HasData(new { CharactersId = 3, MoviesId = 3 }));
		}

	}
}
