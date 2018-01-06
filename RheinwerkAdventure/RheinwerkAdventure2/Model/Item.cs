using Microsoft.Xna.Framework;
using RheinwerkAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    // Basisklasse für alle Spielobjekte
    internal class Item : ICollidable
    {
        public Item()
        {
            Fixed = false;
            Mass = 1f;
        }

        // Bewegungsvector pro Frame
        internal Vector2 move = Vector2.Zero;

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

        // Masse eines Objekts
        public float Mass { get; set; }

        // Ist das Objekt beweglich?
        public bool Fixed { get; set; }
    }
}
