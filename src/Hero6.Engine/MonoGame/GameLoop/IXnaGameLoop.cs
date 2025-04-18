// <copyright file="IXnaGameLoop.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.MonoGame.GameLoop
{
    public interface IXnaGameLoop
    {
        void Initialize();

        void Load();

        void Unload();

        void Update(GameTime time);

        void Draw(GameTime time);
    }
}
