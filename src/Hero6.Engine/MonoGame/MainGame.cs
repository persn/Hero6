// <copyright file="MainGame.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Diagnostics;
using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.Settings;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.MonoGame
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class MainGame : Game
    {
        private readonly ILogger logger;
        private readonly IGameSettings gameSettings;

        public MainGame(
            ILogger<MainGame> logger,
            IGameSettings gameSettings,
            IUserSettings userSettings)
        {
            Thread.CurrentThread.Name = "Hero6 Engine Main Thread";

            this.logger = logger;
            this.gameSettings = gameSettings;

            Content.RootDirectory = "Content";
            Window.Title = "Hero6";
            GraphicsDeviceManager = new GraphicsDeviceManager(this);
            GraphicsDeviceManager.DeviceCreated += (s, a) =>
            {
                GraphicsDeviceManager.PreferredBackBufferWidth = userSettings.WindowWidth;
                GraphicsDeviceManager.PreferredBackBufferHeight = userSettings.WindowHeight;
                GraphicsDeviceManager.IsFullScreen = userSettings.IsFullScreen;
                GraphicsDeviceManager.GraphicsProfile = GraphicsProfile.Reach;
#if ANDROID
                GraphicsDeviceManager.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
#endif
                GraphicsDeviceManager.ApplyChanges();

                logger.LogInformation("Graphics Device Created.");
                logger.LogInformation($"Graphics Adapter Width {GraphicsDevice.Adapter.CurrentDisplayMode.Width}");
                logger.LogInformation($"Graphics Adapter Height {GraphicsDevice.Adapter.CurrentDisplayMode.Height}");
                logger.LogInformation($"Graphics Adapter Aspect Ratio {GraphicsDevice.Adapter.CurrentDisplayMode.AspectRatio}");
            };
        }

        public static SpriteBatch UISpriteBatch { get; private set; }

        public GraphicsDeviceManager GraphicsDeviceManager { get; private set; }

        public Matrix TransformMatrix { get; private set; } = Matrix.Identity;

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            logger.LogInformation("Initializing game.");
            var sw = Stopwatch.StartNew();

            UpdateScale();
            base.Initialize();

            sw.Stop();
            logger.LogInformation($"Game initialized after {sw.Elapsed}.");
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            logger.LogInformation("Loading game content.");
            var sw = Stopwatch.StartNew();

            UISpriteBatch = new SpriteBatch(GraphicsDevice);
            base.LoadContent();

            sw.Stop();
            logger.LogInformation($"Game content loaded after {sw.Elapsed}.");
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            logger.LogInformation("Unloading game content.");
            var sw = Stopwatch.StartNew();

            Content.Unload();
            base.UnloadContent();

            sw.Stop();
            logger.LogInformation($"Game content unloaded after {sw.Elapsed}.");
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="time">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime time)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);
            base.Draw(time);
        }

        private void UpdateScale()
        {
            var horScaling = GraphicsDevice.PresentationParameters.BackBufferWidth / gameSettings.NativeWidth;
            var verScaling = GraphicsDevice.PresentationParameters.BackBufferHeight / gameSettings.NativeHeight;
            TransformMatrix = Matrix.CreateScale(horScaling, verScaling, 1.0f);
            _ = TransformMatrix.Decompose(out var scale, out _, out _);
            gameSettings.WindowScale = scale.ToDotNet();
        }
    }
}
