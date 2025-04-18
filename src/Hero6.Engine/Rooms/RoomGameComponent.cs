// <copyright file="RoomGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Rooms;

internal class RoomGameComponent(MainGame game, IRepository<IRoom> rooms, IServiceProvider serviceProvider, IGameSettings gameSettings)
    : DrawableEntityBase<IRoom>(MonoGame.UpdateOrder.Room, MonoGame.DrawOrder.Room, game, rooms, serviceProvider)
{
    private readonly IGameSettings gameSettings = gameSettings;

    public override void Update(GameTime gameTime)
    {
        if (!gameSettings.IsPaused)
            base.Update(gameTime);
    }
}
