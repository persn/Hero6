// <copyright file="GraphicsDeviceService.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Services.DependencyInjection;
using LateStartStudio.Hero6.Services.Logger;
using LateStartStudio.Hero6.Services.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Services.Graphics
{
    [Injectable]
    public class GraphicsDeviceService : IGraphicsDeviceService
    {
        private readonly GraphicsDeviceManager graphics;

        public GraphicsDeviceService(GraphicsDeviceManager graphics, IUserSettings userSettings, ILogger logger)
        {
            this.graphics = graphics;

            WindowWidth = userSettings.WindowWidth;
            WindowHeight = userSettings.WindowHeight;
            IsFullScreen = userSettings.IsFullScreen;
            Apply();

            logger.Info("Graphics Device Created.");
            logger.Info($"Graphics Adapter Width {graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Width}");
            logger.Info($"Graphics Adapter Height {graphics.GraphicsDevice.Adapter.CurrentDisplayMode.Height}");
            logger.Info($"Graphics Adapter Aspect Ratio {graphics.GraphicsDevice.Adapter.CurrentDisplayMode.AspectRatio}");
        }

        public int WindowWidth
        {
            get => graphics.PreferredBackBufferWidth;
            set => graphics.PreferredBackBufferWidth = value;
        }

        public int WindowHeight
        {
            get => graphics.PreferredBackBufferHeight;
            set => graphics.PreferredBackBufferHeight = value;
        }

        public bool IsFullScreen
        {
            get => graphics.IsFullScreen;
            set => graphics.IsFullScreen = value;
        }

        public Texture2D MakeTexture(Color color)
        {
            var result = new Texture2D(graphics.GraphicsDevice, 1, 1);
            result.SetData(new[] { color });
            return result;
        }

        public void Apply() => graphics.ApplyChanges();
    }
}
