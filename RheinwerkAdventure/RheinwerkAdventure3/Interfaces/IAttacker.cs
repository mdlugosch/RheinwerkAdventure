using RheinwerkAdventure.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Interfaces
{
    interface IAttacker
    {
        // Liste von Objekten die angreifbar sind
        ICollection<Item> AttackableItems { get; }

        // Angriffsreichweite
        float AttackRange { get; }

        // Angriffsstärke
        int AttackValue { get; }
    }
}
