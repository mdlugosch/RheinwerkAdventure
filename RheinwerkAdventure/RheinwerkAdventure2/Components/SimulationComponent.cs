using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Components
{
    internal class SimulationComponent : GameComponent
    {
        private RheinwerkGame game;

        public SimulationComponent(RheinwerkGame game) : base(game)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}
