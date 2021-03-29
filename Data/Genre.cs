using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace GameLibraryApi.Data
{
    [Serializable]
    public class Genre
    {
        public Genre()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        [JsonIgnore]
        public ICollection<Game> Games { get; set; }
    }
}
