using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    // Basisklasse für alle Spielobjekte
    class Item
    {
        public Vector2 Position
        {
            get;
            private set;
        }

        public float Radius
        {
            get;
            private set;
        }

        public Item()
        {
        }
    }
}
