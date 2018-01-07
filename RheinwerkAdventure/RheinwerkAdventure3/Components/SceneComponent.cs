using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RheinwerkAdventure.Model;

namespace RheinwerkAdventure.Components
{
    internal class SceneComponent : DrawableGameComponent
    {
        private SpriteBatch spriteBatch;
        private Texture2D pixel;
        private RheinwerkGame game;

        public SceneComponent(RheinwerkGame game) : base(game)
        {
            this.game = game;
        }

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            // Pixel für den Zeichnungsprozess generieren
            pixel = new Texture2D(GraphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White });
        }

        public override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            Area area = game.Simulation.World.Areas[0];

            /*
             * (Verfügbarerfläche - Rand) / notwendige Fläche = Skalierungsfaktor
             * PixelPosition * Skalierungsfaktor = Zeichnungsposition
             */
            float scaleX = (GraphicsDevice.Viewport.Width - 20) / area.Width;
            float scaleY = (GraphicsDevice.Viewport.Height - 20) / area.Height;

            spriteBatch.Begin();

            // Spielfeld zeichnen
            for (int x = 0; x < area.Width; x++)
            {
                for (int y = 0; y < area.Height; y++)
                {
                    bool blocked = false;
                    for (int l = 0; l < area.Layers.Length; l++)
                    {
                        blocked |= area.Layers[l].Tiles[x,y].Blocked;
                    }

                    Color color = Color.DarkGreen;
                    if(blocked) color = Color.DarkRed;

                    // Offset für den Versatz der einzelnen Flächen. Inklusive Rand.
                    int offsetX = (int)(x * scaleX) + 10;
                    int offsetY = (int)(y * scaleY) + 10;

                    /*
                     * Testtile erzeugen Annahme: 
                     * Die Tilegröße entspricht einer Schrittweite(Skalierungsfaktor)
                     * | offsetY
                     * | offsetX    -   -   -   -   ->scaleX
                     * |            X   X   X   X
                     * |            X   X   X   X
                     * |            X   X   X   X
                     * |            X   X   X   X
                     * V
                     * scaleY
                     */
                    // Zeichnen der Tilefläche in Grün.
                    spriteBatch.Draw(pixel, new Rectangle(offsetX,offsetY,(int)scaleX,(int)scaleY), color);
                    // Linken und oberen Rand zeichnen in Schwarz.
                    spriteBatch.Draw(pixel, new Rectangle(offsetX, offsetY, 1, (int)scaleY), Color.Black);
                    spriteBatch.Draw(pixel, new Rectangle(offsetX, offsetY, (int)scaleX, 1), Color.Black);
                }
            }

            // Items auf Spielfeld zeichnen
            foreach (var item in area.Items)
            {
                Color color = Color.Yellow;
                if (item is Player)
                    color = Color.Red;

                /*
                 * Zeichenposition ermittel. MittelpunkX bzw. Y - ItemRadius = linke obere Ecke vom Item.
                 */
                int posX = (int)((item.Position.X - item.Radius) * scaleX) + 10;
                int posY = (int)((item.Position.Y - item.Radius) * scaleY) + 10;
                int size = (int)((item.Radius * 2) * scaleX);
                spriteBatch.Draw(pixel, new Rectangle(posX, posY, size, size), color);
            }

            spriteBatch.End();

            base.Draw(gameTime);
        }
    }
}
