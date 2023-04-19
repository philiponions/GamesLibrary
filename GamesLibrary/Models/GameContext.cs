﻿using System;
using Microsoft.EntityFrameworkCore;
namespace GamesLibrary.Models
{
	public class GameContext : DbContext
	{
		public GameContext(DbContextOptions<GameContext> options) : base(options)
		{
		}

		public DbSet<Game> Games { get; set; } = null;
	}
}

