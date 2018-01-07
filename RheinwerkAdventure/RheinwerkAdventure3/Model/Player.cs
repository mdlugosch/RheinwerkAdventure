using RheinwerkAdventure.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Model
{
    class Player : Character, IAttackable, IAttacker
    {

        public Player()
        {
            AttackableItems = new List<Item>();

            // TODO: Default Values
        }

        public ICollection<Item> AttackableItems { get; private set; }

        public float AttackRange { get; set; }

        public int AttackValue { get; set; }

        public int Hitpoints { get; set; }

        public int MaxHitpoints { get; set; }
    }
}
