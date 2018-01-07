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

        public Vector2 Movement { get; private set; }

        public InputComponent(RheinwerkGame game) : base(game)
        {
            this.game = game;
        }

        public override void Update(GameTime gameTime)
        {
            Vector2 movement = Vector2.Zero;

            // Gamepad Steuerung
            movement += GamePad.GetState(PlayerIndex.One).ThumbSticks.Left * new Vector2(1f, -1f);

            // Keyboard Steuerung
            KeyboardState keyboard = Keyboard.GetState();
            if (keyboard.IsKeyDown(Keys.Left))
                movement += new Vector2(-1f, 0f);
            if (keyboard.IsKeyDown(Keys.Right))
                movement += new Vector2(1f, 0f);
            if (keyboard.IsKeyDown(Keys.Up))
                movement += new Vector2(0f, -1f);
            if (keyboard.IsKeyDown(Keys.Down))
                movement += new Vector2(0f, 1f);

            /*
             * Vector darf nicht größer 1 sein. Mit der Normalisierungsmethode
             * wird in einem solchen Fall der Vector auf 1 gekürzt. Dies kann zB.
             * vorkommen wenn der Spieler Eins nach oben und Eins nach links steuert.
             */
            if (movement.Length() > 1)
                movement.Normalize();

            // Berechneten Wert der Property übergeben.
            Movement = movement;

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                game.Exit();

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
    }
}
