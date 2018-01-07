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

        public List<Item> Items
        {
            get;
            private set;
        }

        public Layer[] Layers
        {
            get;
            private set;
        }

        // Area wird direkt mit Höhen- und Breitenangaben initialisiert.
        public Area(int layers, int width, int height)
        {
            if (width < 5)
                throw new ArgumentException("Spielbereich muss mindestens 5 Zellen breit sein");
            if (height < 5)
                throw new ArgumentException("Spielbereich muss mindestens 5 Zellen hoch sein");


            // Höhen- und Breitenangaben in Properties ablegen
            Width = width; Height = height;

            Layers = new Layer[layers];
            for(int l = 0; l < layers; l++)
            {
                Layers[l] = new Layer(width, height);
            }

            // Tilemap erzeugen und Items erzeugen
            Items = new List<Item>();
        }

        // Alle Layer des Areas auf eine geblockte Position prüfen
        public bool IsCellBlocked(int x, int y)
        {
            /* Sonderfall:
             * Prüfen ob Position ausserhalb des Bereichs ist.
             * In diesem Fall gilt die abgefragte Position als blockiert.
             */
            if (x < 0 || y < 0 || x > Width - 1 || y > Height - 1) return true;

            for(int l = 0; l < Layers.Length;l++)
            {
                if (Layers[l].Tiles[x, y].Blocked) return true;
            }

            return false;
        }
    }
}
