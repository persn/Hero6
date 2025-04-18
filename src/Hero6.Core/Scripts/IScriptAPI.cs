// <copyright file="IScriptAPI.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Animations;
using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.UserInterfaces;

namespace LateStartStudio.Hero6.Scripts;

public interface IScriptAPI
{
    IRepository<IAnimation> Animations { get; }

    IRepository<ICharacterAnimation> CharacterAnimations { get; }

    ICharacterRepository Characters { get; }

    IRepository<IInventoryItem> InventoryItems { get; }

    IRepository<IItem> Items { get; }

    IRepository<IRoom> Rooms { get; }

    IRepository<ICursor> Cursors { get; }

    IRepository<IWindow> Windows { get; }

    void ShowMessage(string message);
}
