// <copyright file="RoomGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;

namespace LateStartStudio.Hero6.Rooms;

internal class RoomGameComponent : DrawableEntityBase<Room>
{
    public RoomGameComponent(Room room, MainGame game)
        : base(room, MonoGame.UpdateOrder.Room, MonoGame.DrawOrder.Room, game)
    {
    }
}
