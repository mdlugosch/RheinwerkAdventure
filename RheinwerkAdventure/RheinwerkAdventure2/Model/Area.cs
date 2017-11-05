using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    // Enthält die Struktur eines einzelnen Teilbereichs der Karte
    class Area
    {
        public List<Tile> Tiles
        {
            get;
            private set;
        }

        public List<Item> Items
        {
            get;
            private set;
        }

        public Area()
        {
            Tiles = new List<Tile>();
            Items = new List<Item>();
        }
    }
}
