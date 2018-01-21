using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RheinwerkAdventure.Model;
using RheinwerkAdventure.Interfaces;

namespace RheinwerkAdventure.Components
{
    internal class SimulationComponent : GameComponent
    {
        // Korrekturwert für das Rundungsproblem bei der Kollisionsabfrage
        private float gap = 0.00001f;

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

            #region item to item Kollisionen
            foreach (var area in World.Areas)
            {
                foreach (var character in area.Items.OfType<Character>())
                {
                    // Bewegungsvektor des Objekts berechnen und ablegen
                    character.move += character.Velocity * (float)gameTime.ElapsedGameTime.TotalSeconds;

                    IAttacker attacker = null;
                    if(character is IAttacker)
                    {
                        attacker = (IAttacker)character;
                        attacker.AttackableItems.Clear();
                    }

                    // Position auf Kollision prüfen
                    foreach (var item in area.Items)
                    {
                        /*
                         * Der Charakter würde immer mit sich selbst kollidieren
                         * daher muss eine prüfung auf sich selbst ausgeschlossen werden. 
                         */
                        if (item == character) continue;

                        Vector2 distance = (item.Position + item.move) - (character.Position + character.move);
                        float overlap = item.Radius + character.Radius - distance.Length();

                        // Ween sich in Angriffsreichweite ein angreifbares Item befindet wird es der List hinzugefügt
                        if (item is IAttackable && distance.Length() - attacker.AttackRange - item.Radius < 0)    
                        {
                                attacker.AttackableItems.Add(item);
                        }

                        // Wenn overlap > 0 dann liegt eine Kollision vor.
                        if (overlap > 0f)
                        {
                            Vector2 resolution = distance * (overlap / distance.Length());

                            // Nach Kollisionsart unterscheiden (bewegliche und unbewegliche Objekte)
                            if (item.Fixed && !character.Fixed)
                            {
                                // Item fixiert
                                character.move -= resolution;
                            }
                            else if (!item.Fixed && character.Fixed)
                            {
                                // Character fixiert
                                item.move += resolution;
                            }
                            else if (!item.Fixed && !character.Fixed)
                            {
                                // keiner ist fixiert
                                float totalMass = item.Mass + character.Mass;

                                // Verschiebung nach Masseverhältniss berechnen
                                character.move -= resolution * (item.Mass / totalMass);
                                item.move += resolution * (character.Mass / totalMass);
                            }
                        }
                    }
                }
                #endregion

                    #region  Kollisionsprüfung mit den Wänden
                    // Bewegung durchführen und Korrekturen durchführen
                    foreach (var item in area.Items)
                    {
                        bool collision = false;
                        int loops = 0;
                    
                        do
                        {
                            Vector2 position = item.Position + item.move;
                    
                            // Kleinster/Größter X-Achsenwert eines Objekts
                            int minCellX = (int)(position.X - item.Radius);
                            int maxCellX = (int)(position.X + item.Radius);
                    
                            // Kleinster/Größter Y-Achsenwert eines Objekts
                            int minCellY = (int)(position.Y - item.Radius);
                            int maxCellY = (int)(position.Y + item.Radius);
                    
                            float minImpact = 2f;
                            int minAxis = 0;
                            collision = false;
                    
                            for (int x = minCellX; x <= maxCellX; x++)
                            {
                                for (int y = minCellY; y <= maxCellY; y++)
                                {
                                    // Ist die Zelle blockiert?
                                    // Wenn nicht dann Continue.
                                    if (!area.IsCellBlocked(x, y)) continue;

                                    if (position.X - item.Radius > x + 1 ||
                                       position.X + item.Radius < x ||
                                       position.Y - item.Radius > y + 1 ||
                                       position.Y + item.Radius < y) continue;

                                    collision = true;

                                    /* Überschneidung auf der X-Achse prüfen:
                                    *  diffX muss initialisiert werden. Das Passiert mit dem Maximalwert.
                                    *  Wir wissen an dieser Stelle das es eine Kollision gab nur noch nicht den
                                    *  genauen Wert.
                                    */
                                    float diffX = float.MaxValue;
                                    /* 
                                     * bewegt sich das Objekt nach rechts oder links?
                                     * Bewegung nach rechts: Rechte Kante des Objekts - linke Kante der Zelle
                                     * Bewegung nach links: Linke Kante des Objekts - rechte Kante der Zelle
                                     * Rundungsfehler bei der Verwendung von Floats werden mit einem Korrekturwert(gap) berrichtigt.
                                     */
                                    if (item.move.X > 0) diffX = position.X + item.Radius - x + gap;
                                    if (item.move.X < 0) diffX = position.X - item.Radius - (x + 1) - gap;

                                    // Berechnung des Objektabstandes bevor es zur Kollision kommt.
                                    float impactX = 1f - (diffX / item.move.X);

                                    // Überschneidung auf der Y-Achse prüfen
                                    float diffY = float.MaxValue;
                                    /* 
                                     * bewegt sich das Objekt nach rechts oder links?
                                     * Bewegung nach rechts: Rechte Kante des Objekts - linke Kante der Zelle
                                     * Bewegung nach links: Linke Kante des Objekts - rechte Kante der Zelle
                                     */
                                    if (item.move.Y > 0) diffY = position.Y + item.Radius - y + gap;
                                    if (item.move.Y < 0) diffY = position.Y - item.Radius - (y + 1) - gap;

                                    // Berechnung des Objektabstandes bevor es zur Kollision kommt.
                                    float impactY = 1f - (diffY / item.move.Y);

                                    /*
                                     * Beide Achsen müssen kollidieren damit eine Kollision vorliegt.
                                     * 1) Welche Achse hat zuletzt die Kollision verursacht?
                                     * 2) Wieweit kann der Bewegungsvektor angewendet werden bis es zur Kollision kommt?
                                     * axis = 1 (X-Achse verursacht zuerst die Kollision)
                                     * axis = 2 (Y-Achse verursacht zuerst die Kollision)
                                     * impact speichert den Restwert der Bewegung
                                     */
                                    int axis = 0;
                                    float impact = 0;
                                    if (impactX > impactY)
                                    {
                                        axis = 1;
                                        impact = impactX;
                                    }
                                    else
                                    {
                                        axis = 2;
                                        impact = impactY;
                                    }

                                    /* Ersten Einschlag ermitteln:
                                     * Ist diese Kollision eher als die bisher erkannten?
                                     */
                                    if (impact < minImpact)
                                    {
                                        minImpact = impact;
                                        minAxis = axis;
                                    }
                                }
                            }

                            if (collision)
                            {
                                // Kollisionsachse mit Kürzungvektor multiplizieren
                               if (minAxis == 1) item.move *= new Vector2(minImpact, 1f);
                                if (minAxis == 2) item.move *= new Vector2(1f, minImpact);
                            }
                            loops++;
                        } while (collision && loops < 2); // Solange Kollisionen entstehen werden Kollisionen weiter berechnet bis zu einem maximalen Durchlauf von 2.
                    #endregion

                    // Neue Itemposition berechnen
                    item.Position += item.move;
                    // Bewegungsvektor wieder zurücksetzen
                    item.move = Vector2.Zero;
                }
            }
                    base.Update(gameTime);
            }             

        public void NewGame()
        {
            // Welt erzeugen
            World = new World();

            // Gebiet erzeugen
            Area area = new Area(2, 30, 20);

            // Tiles im Area initialisieren
            for (int x = 0; x < area.Width; x++)
            {
                for (int y = 0; y < area.Height; y++)
                {
                    area.Layers[0].Tiles[x, y] = new Tile();
                    area.Layers[1].Tiles[x, y] = new Tile();

                    if (x == 0 || y == 0 || x == area.Width - 1 || y == area.Height - 1)
                        area.Layers[0].Tiles[x, y].Blocked = true;
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
