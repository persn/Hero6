// <copyright file="MouseGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.UserInterfaces;

internal class MouseGameComponent(MainGame game, IRepository<ICursor> cursors, IServiceProvider serviceProvider, IMouse mouse)
    : DrawableEntityBase<ICursor>(MonoGame.UpdateOrder.Cursor, MonoGame.DrawOrder.Cursor, game, cursors, serviceProvider)
{
    private readonly Mouse mouse = (Mouse)mouse;

    public override void Initialize()
    {
        mouse.Initialize();
        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        mouse.Update(gameTime);
        base.Update(gameTime);
    }
}
