﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using RheinwerkAdventure.Components;

namespace RheinwerkAdventure
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class RheinwerkGame : Game
    {
        GraphicsDeviceManager graphics;

        // Zugriff auf Component Klassen über Properties
        public HudComponent Hud { get; private set; }
        internal InputComponent Input { get; private set; }
        internal SceneComponent Scene { get; private set; }
        internal SimulationComponent Simulation { get; private set; }

        public RheinwerkGame()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            graphics.IsFullScreen = false;
            // Fenstergrösse festlegen.
            graphics.PreferredBackBufferHeight = 2000;
            graphics.PreferredBackBufferWidth = 3000;

            // Objektinstanzen erzeugen
            Input = new InputComponent(this);
            Input.UpdateOrder = 0;

            Scene = new SceneComponent(this);
            Scene.UpdateOrder = 2;
            Scene.DrawOrder = 0;

            Simulation = new SimulationComponent(this);
            Simulation.UpdateOrder = 1;

            // Komponenten hinzufügen
            Components.Add(Input);
            Components.Add(Scene);
            Components.Add(Simulation);

            // Update Order bestimmt in welcher Reihenfolge die Komponenten gemalt werden
            Input.UpdateOrder = 0;
            Simulation.UpdateOrder = 1;
            Scene.UpdateOrder = 2;

            Hud = new HudComponent(this);
            Hud.UpdateOrder = 3;
            Hud.DrawOrder = 1;
            Components.Add(Hud);
        }

        // Nicht verwendet
        //protected override void Initialize()
        //{
            // TODO: Add your initialization logic here
        //    base.Initialize();
        //}

        //protected override void UnloadContent()
        //{
            // TODO: Unload any non ContentManager content here
        //}

    }
}
