using System;
using System.Collections.Generic;


namespace GameLibraryApi.Data
{
    [Serializable]
    public class Game
    {
        public Game()
        {
            Genres = new HashSet<Genre>();
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Autor { get; set; }



        public ICollection<Genre> Genres { get; set; }

    }

}
