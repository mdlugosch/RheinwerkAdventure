using RheinwerkAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    class Player : Character, IAttackable
    {

        public Player()
        {
        }

        int IAttackable.Hitpoints
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
