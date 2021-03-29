using GameLibraryApi.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GameLibraryApi.Services
{
    public interface IGameStore
    {
        Task<IEnumerable<Game>> SelectAsync(int offset, int count);
        Task<Game> SelectAsync(int id);
        Task<int> UpdateAsync(Game game);
        Task<Game> InsertAsync(Game game);
        Task<int> DeleteAsync(Game game);
        Task<IEnumerable<Game>> SelectByGenreAsync(string genre, int offset, int count);

    }
}
