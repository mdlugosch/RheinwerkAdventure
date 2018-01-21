using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RheinwerkAdventure.Components
{
    // Klasse für das Zeichnen der Hud-Komponenten
    public class HudComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        RheinwerkGame game;
        SpriteFont hudFont;

        public HudComponent(RheinwerkGame game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            hudFont = Game.Content.Load<SpriteFont>("HudFont");
        }

        public override void Draw(GameTime gameTime)
        {
            spriteBatch.Begin();
            spriteBatch.DrawString(hudFont, "Development Version", new Vector2(20, 20), Color.White);
            spriteBatch.End();
        }
    }
}
