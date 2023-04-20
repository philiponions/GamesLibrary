using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GamesLibrary.Models;

namespace GamesLibrary.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesController : ControllerBase
    {
        private readonly GameContext _context;

        public GamesController(GameContext context)
        {
            _context = context;
        }

        // GET: api/Games
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(int pageNumber = 1, int pageSize = 10)
        {
          if (_context.Games == null)
          {
              return NotFound();
          }


            // Skip() method used to skip appropriate number of games based on page number and page size
            // Take() Method used to take only the specified number of games
            var games = await _context.Games.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
            return games;
        }

        // GET: api/Games
        [HttpGet("price")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames([FromQuery] string condition, [FromQuery] float price)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }

            IQueryable<Game> games = _context.Games;

            if (!string.IsNullOrEmpty(condition))
            {
                switch (condition.ToLower())
                {
                    case "lessthan":
                        games = games.Where(g => g.Price < price);
                        break;
                    case "greaterthan":
                        games = games.Where(g => g.Price > price);
                        break;
                    case "equalto":

                        // Doing == is not taking into account floating point precision
                        // Check difference and only output if its small enough
                        games = games.Where(g => Math.Abs(g.Price - price) < 0.0001);
                        break;
                }
            }

            return await games.ToListAsync();
        }

        /// <summary>
        /// Deletes a specific TodoItem.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("brand/{brand}")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(string brand)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }

            IQueryable<Game> games = _context.Games;

            // Format params to format in sql
            if (!string.IsNullOrEmpty(brand))
            {
                switch (brand)
                {
                    case "ps5":
                        brand = "PS5";
                        break;
                    case "xsx":
                        brand = "Xbox Series X";
                        break;
                    case "ps4":
                        brand = "PS4";
                        break;
                    case "xb1":
                        brand = "Xbox One";
                        break;
                    case "switch":
                        brand = "Switch";
                        break;
                    case "pc":
                        brand = "PC";
                        break;
                }

                // Once the brand is update, find the game.
                games = games.Where(g => g.Brand == brand);
            }

            return await games.ToListAsync();
        }

        [HttpGet("name")]
        public async Task<ActionResult<IEnumerable<Game>>> GetGameByName(string searchString)
        {
            if (_context.Games == null)
            {
                return NotFound();
            }

            IQueryable<Game> games = _context.Games;

            if (!string.IsNullOrEmpty(searchString))
            {
                // Check if the name of the game contains the string
                games = games.Where(g => g.Name.ToLower().Contains(searchString.ToLower()));
            }

            return await games.ToListAsync();
        }

        // GET: api/Games/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Game>> GetGameById(string id)
        {
          if (_context.Games == null)
          {
              return NotFound();
          }
            // Grabs a game by id
            var game = await _context.Games.FindAsync(id);

            if (game == null)
            {
                return NotFound();
            }

            return game;
        }

        private bool GameExists(string id)
        {
            return (_context.Games?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
