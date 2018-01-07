using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Interfaces
{
    public interface IAttackable
    {
        int MaxHitpoints { get; }

        int Hitpoints { get; }

    }
}
