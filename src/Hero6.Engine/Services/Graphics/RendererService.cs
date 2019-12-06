// <copyright file="RendererService.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Extensions;
using LateStartStudio.Hero6.Services.DependencyInjection;
using LateStartStudio.Hero6.Services.Settings;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Services.Graphics
{
    [Injectable]
    public class RendererService : IRendererService
    {
        private static readonly Color DefaultBackgroundColor = Color.White;

        private readonly SpriteBatch spriteBatch;
        private readonly IGraphicsDeviceService graphicsDevice;
        private readonly IGameSettings gameSettings;

        private Matrix transform = Matrix.Identity;

        public RendererService(SpriteBatch spriteBatch, IGraphicsDeviceService graphicsDevice, IGameSettings gameSettings)
        {
            this.spriteBatch = spriteBatch;
            this.graphicsDevice = graphicsDevice;
            this.gameSettings = gameSettings;

            UpdateScale();
        }

        public void Begin() => spriteBatch.Begin(SpriteSortMode.Deferred, transformMatrix: transform);

        public void End() => spriteBatch.End();

        public void Draw(Texture2D sprite, Vector2 position) => spriteBatch.Draw(sprite, position, DefaultBackgroundColor);

        public void Draw(Texture2D sprite, Rectangle destination, Color background) => spriteBatch.Draw(sprite, destination, background);

        public void Draw(Texture2D sprite, Rectangle source, Rectangle destination) => spriteBatch.Draw(sprite, destination, source, DefaultBackgroundColor);

        public void Draw(SpriteFont font, string text, Vector2 position, Color foreground) => spriteBatch.DrawString(font, text, position, foreground);

        private void UpdateScale()
        {
            var horScaling = graphicsDevice.WindowWidth / gameSettings.NativeWidth;
            var verScaling = graphicsDevice.WindowHeight / gameSettings.NativeHeight;
            transform = Matrix.CreateScale(horScaling, verScaling, 1.0f);
            gameSettings.WindowScale = transform.Scale().ToDotNet();
        }
    }
}
