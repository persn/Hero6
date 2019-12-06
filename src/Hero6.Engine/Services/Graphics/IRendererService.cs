// <copyright file="IRendererService.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Services.Graphics
{
    /// <summary>
    /// Wrapper around graphics drawing for MonoGame.
    /// </summary>
    public interface IRendererService
    {
        void Begin();

        void End();

        void Draw(Texture2D sprite, Vector2 position);

        void Draw(Texture2D sprite, Rectangle destination, Color background);

        void Draw(Texture2D sprite, Rectangle source, Rectangle destination);

        void Draw(SpriteFont font, string text, Vector2 position, Color foreground);
    }
}
