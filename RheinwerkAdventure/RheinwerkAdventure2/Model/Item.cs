using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    // Basisklasse für alle Spielobjekte
    internal class Item
    {
        public Vector2 Position
        {
            get;
            set;
        }

        public float Radius
        {
            get;
            set;
        }

        public Item()
        {
        }
    }
}
