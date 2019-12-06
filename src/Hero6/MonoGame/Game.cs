// <copyright file="Game.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.DependencyInjection;
using LateStartStudio.Hero6.Services.Graphics;
using LateStartStudio.Hero6.Services.Logger;
using LateStartStudio.Hero6.Services.Settings;
using LateStartStudio.Hero6.Services.UserInterfaces;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.MonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game : Microsoft.Xna.Framework.Game
    {
        private static Logger logger;

        private IGameSettings gameSettings;
        private MonoGameUserInterfaces ui;
        private MonoGameCampaigns campaigns;
        private IRendererService renderer;

        private Game()
        {
            Content.RootDirectory = "Content";
            Window.Title = "Hero6";

            var graphics = new GraphicsDeviceManager(this)
            {
                GraphicsProfile = GraphicsProfile.Reach,
#if ANDROID
                SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight
#endif
            };
            graphics.DeviceCreated += (s, a) =>
            {
                Container = new Container(Content, graphics);
                var userSettings = Container.Get<IUserSettings>();
                logger = (Logger)Container.Get<ILogger>();
                gameSettings = Container.Get<IGameSettings>();
                ui = (MonoGameUserInterfaces)Container.Get<IUserInterfaces>();
                campaigns = (MonoGameCampaigns)Container.Get<ICampaigns>();
                renderer = Container.Get<IRendererService>();

                GraphicsDeviceCreated?.Invoke(s, a);
            };
        }

        public event EventHandler<EventArgs> GraphicsDeviceCreated;

        public IContainer Container { get; private set; }

        public static void Start(Action<Game> onStart)
        {
            try
            {
                using var game = new Game();
                onStart(game);
            }
#if !DEBUG
            catch (Exception e)
            {
                logger.Error("Hero6 has crashed, logging stack trace.");
                logger.Exception(e);
                logger.WillDeleteLogOnDispose = false;
                var p = new System.Diagnostics.Process { StartInfo = { UseShellExecute = true, FileName = logger.Filename } };
                p.Start();
            }
#endif
            finally
            {
            }
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            logger.Initialize();

            ui.Initialize();
            campaigns.Initialize();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            ui.Load();
            campaigns.Load();
            base.LoadContent();
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            Content.Unload();
            ui.Unload();
            campaigns.Unload();
            base.UnloadContent();
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime time)
        {
            ui.Update(time);

            if (!gameSettings.IsPaused)
            {
                campaigns.Update(time);
            }

            base.Update(time);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            renderer.Begin();
            campaigns.Draw(time);
            ui.Draw(time);
            renderer.End();

            base.Draw(time);
        }
    }
}
