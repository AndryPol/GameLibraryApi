using GameLibraryApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryApi.Services.Implementation
{
    public class GameStore : IGameStore
    {
        private readonly GameLibraryApiContext _gameLibraryApiContext;

        public GameStore(GameLibraryApiContext gameLibraryApiContext)
        {
            _gameLibraryApiContext = gameLibraryApiContext;
        }

        public Task<Game> SelectAsync(int id)
        {
            return _gameLibraryApiContext.Games.Include(_ => _.Genres).Where(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Game> InsertAsync(Game game)
        {
            var genres = game.Genres.ToArray();

            for (int i = 0; i < game.Genres.Count; i++)
            {
                Genre res = await _gameLibraryApiContext.Genres.FirstOrDefaultAsync(_ => _.Name == genres[i].Name);
                if (res == null)
                {
                    _gameLibraryApiContext.Genres.Add(genres[i]);
                }
                else genres[i] = res;
            }

            game.Genres = genres;

            await _gameLibraryApiContext.SaveChangesAsync();

            _gameLibraryApiContext.Games.Add(game);

            await _gameLibraryApiContext.SaveChangesAsync();

            return game;
        }



        public async Task<int> UpdateAsync(Game game)
        {
            _gameLibraryApiContext.Entry(game).State = EntityState.Modified;

            return await _gameLibraryApiContext.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(Game game)
        {
            _gameLibraryApiContext.Games.Remove(game);

            return await _gameLibraryApiContext.SaveChangesAsync();
        }

        public Task<IEnumerable<Game>> SelectAsync(int offset, int count)
        {
            return Task.FromResult(_gameLibraryApiContext.Games.Include(_ => _.Genres).Skip(offset).Take(count).AsEnumerable());
        }

        public Task<IEnumerable<Game>> SelectByGenreAsync(string genre, int offset, int count)
        {
            IEnumerable<Game> games = new List<Game>();

            var genre_res = _gameLibraryApiContext.Genres.FirstOrDefault(_ => _.Name == genre);

            if (genre_res != null)

                games = _gameLibraryApiContext.Games.Include(_ => _.Genres).Where(_ => _.Genres.FirstOrDefault(_ => _.Id == genre_res.Id) != null).Skip(offset).Take(count);

            return Task.FromResult(games);
        }
    }
}
