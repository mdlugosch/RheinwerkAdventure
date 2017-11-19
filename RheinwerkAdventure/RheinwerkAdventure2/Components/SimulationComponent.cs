using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RheinwerkAdventure.Model;

namespace RheinwerkAdventure.Components
{
    internal class SimulationComponent : GameComponent
    {
        private RheinwerkGame game;

        public World World
        {
            get;
            private set;
        }

        public Player Player
        {
            get;
            private set;
        }

        public SimulationComponent(RheinwerkGame game) : base(game)
        {
            this.game = game;

            // Spielwelt erzeugen
            NewGame();
        }

        public override void Update(GameTime gameTime)
        {

            # region Player Input

            Player.Velocity = game.Input.Movement * 10f;

            #endregion

            #region Chracter Movement

            foreach(var area in World.Areas)
            {
                foreach (var character in area.Items.OfType<Character>())
                {
                    character.Position += character.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;
                }
            }

            #endregion

            base.Update(gameTime);
        }

        public void NewGame()
        {
            // Welt erzeugen
            World = new World();

            // Gebiet erzeugen
            Area area = new Area(30, 20);

            // Tiles im Area initialisieren
            for (int x = 0; x < area.Width; x++)
            {
                for (int y = 0; y < area.Height; y++)
                {
                    area.Tiles[x, y] = new Tile();
                }
            }

            // Items erzeugen. Player ist hierbei eine Property um externen Zugriff zu ermöglichen.
            Player = new Player() { Position = new Vector2(15, 10), Radius = 0.25f };
            Diamant diamant = new Diamant() { Position = new Vector2(10, 10), Radius = 0.25f };

            // Items dem Gebiet hinzufügen
            area.Items.Add(Player);
            area.Items.Add(diamant);

            // Gebiet der Welt hinzufügen
            World.Areas.Add(area);
        }
    }
}
