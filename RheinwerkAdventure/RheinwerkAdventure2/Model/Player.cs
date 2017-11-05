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
        public int Hitpoints => throw new NotImplementedException();

        public Player()
        {
        }
       
    }
}
