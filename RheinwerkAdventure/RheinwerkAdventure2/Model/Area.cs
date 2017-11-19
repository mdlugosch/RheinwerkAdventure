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
        /*
         * Properties für den Zugriff auf die Höhe und die
         * Breite des Areas.
         */
        public int Width
        {
            get;
            private set;
        }

        public int Height
        {
            get;
            private set;
        }

        /*
         * TileArea besteht aus zwei Dimensionen. Beispiel 20*30 Tiles
         */
        public Tile[,] Tiles
        {
            get;
            private set;
        }

        public List<Item> Items
        {
            get;
            private set;
        }

        // Area wird direkt mit Höhen- und Breitenangaben initialisiert.
        public Area(int width, int height)
        {
            // Höhen- und Breitenangaben in Properties ablegen
            Width = width; Height = height;

            // Tilemap erzeugen und Items erzeugen
            Tiles = new Tile[width,height];
            Items = new List<Item>();
        }
    }
}
