using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Interfaces
{
    internal interface ICollidable
    {
        // Masse eines Objekts
        float Mass { get; }

        // Ist das Objekt beweglich?
        bool Fixed { get; }

    }
}
