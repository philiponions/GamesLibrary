using System;
using Microsoft.EntityFrameworkCore;
namespace GamesLibrary.Models
{
	public class GameContext : DbContext
	{
        public GameContext()
        {
        }

        public GameContext(DbContextOptions<GameContext> options) : base(options)
		{
		}

		public DbSet<Game> Games { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder options)
		{
			// Connect to Sqlite Database
			options.UseSqlite(@"Data Source=library.db");
		}
    }
}

