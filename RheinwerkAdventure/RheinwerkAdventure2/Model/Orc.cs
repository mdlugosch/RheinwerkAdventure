using RheinwerkAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    class Orc : Character, IAttackable
    {

        public Orc()
        {
        }

        public int Hitpoints
        {
            get
            {
                throw new NotImplementedException();
            }
        }
    }
}
