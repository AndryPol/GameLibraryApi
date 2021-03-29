using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using GameLibraryApi.Data;
using GameLibraryApi.Services;

namespace GameLibraryApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GamesController : Controller
    {
        private readonly IGameStore _gameStore;

        public GamesController(IGameStore gameStore)
        {
            _gameStore = gameStore;
        }


        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<Game>> GetGame(int id)
        {
            Game game = await _gameStore.SelectAsync(id);
            if (game == null)
            {
                return NotFound();
            }
            return game;

        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Game>>> GetGames(string genre, int offset, int count)
        {
            IEnumerable<Game> games;
            if (genre == null)
                games = await _gameStore.SelectAsync(offset, count);
            else
                games = await _gameStore.SelectByGenreAsync(genre, offset, count);

            return games.ToList();

        }

        [HttpPut]
        [Route("{id}")]
        public async Task<IActionResult> PutGame(int id, [FromBody] Game game)
        {
            if (id != game.Id)
            {
                return BadRequest();
            }
            await _gameStore.UpdateAsync(game);
            return NoContent();
        }
        [HttpPost]
        public async Task<ActionResult<Game>> PostGame(Game game)
        {
            Game res = await _gameStore.InsertAsync(game);

            return CreatedAtAction(nameof(GetGame), new { id = res.Id }, game);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGame(int id)
        {
            var todoItem = await _gameStore.SelectAsync(id);
            if (todoItem == null)
            {
                return NotFound();
            }

            await _gameStore.DeleteAsync(todoItem);

            return NoContent();
        }


    }
}
