// <copyright file="RoomGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Rooms;

internal class RoomGameComponent(MainGame game, IRepository<IRoom> rooms, IServiceProvider serviceProvider)
    : DrawableEntityBase<IRoom>(MonoGame.UpdateOrder.Room, MonoGame.DrawOrder.Room, game, rooms, serviceProvider)
{
}
