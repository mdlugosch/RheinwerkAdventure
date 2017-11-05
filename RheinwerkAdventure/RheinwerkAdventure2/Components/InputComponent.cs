using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Components
{
    internal class InputComponent : GameComponent
    {
        private RheinwerkGame game;

        public InputComponent(RheinwerkGame game) : base(game)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}
