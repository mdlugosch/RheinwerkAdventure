using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    internal class Layer
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

        public Layer(int width, int height)
        {
            if (width < 5)
                throw new ArgumentException("Spielbereich muss mindestens 5 Zellen breit sein");
            if(height < 5)
                throw new ArgumentException("Spielbereich muss mindestens 5 Zellen hoch sein");

            // Höhen- und Breitenangaben in Properties ablegen
            Width = width; Height = height;

            Tiles = new Tile[width, height];
        }
    }
}
