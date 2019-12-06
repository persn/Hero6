// <copyright file="IGraphicsDeviceService.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Services.Graphics
{
    public interface IGraphicsDeviceService
    {
        int WindowWidth { get; set; }

        int WindowHeight { get; set; }

        bool IsFullScreen { get; set; }

        /// <summary>
        /// Creates a simple texture with dimensions 1x1.
        /// </summary>
        /// <param name="color"></param>
        /// <returns>A <see cref="Texture2d"/> instance with dimenions 1x1 and input color.</returns>
        Texture2D MakeTexture(Color color);

        void Apply();
    }
}
