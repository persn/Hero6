// <copyright file="ScriptAPI.cs" company="Late Start Studio">
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

namespace LateStartStudio.Hero6.Scripts;

internal class ScriptAPI(
    IRepository<IAnimation> animations,
    ICharacterRepository characters,
    IRepository<ICharacterAnimation> characterAnimations,
    IRepository<IInventoryItem> inventoryItems,
    IRepository<IItem> items,
    IRepository<IRoom> rooms,
    IRepository<ICursor> cursors,
    IRepository<IWindow> windows,
    IRepository<Script> scripts) : IScriptAPI
{
    private readonly IRepository<Script> scripts = scripts;

    public IRepository<IAnimation> Animations { get; } = animations;

    public IRepository<ICharacterAnimation> CharacterAnimations { get; } = characterAnimations;

    public ICharacterRepository Characters { get; } = characters;

    public IRepository<IInventoryItem> InventoryItems { get; } = inventoryItems;

    public IRepository<IItem> Items { get; } = items;

    public IRepository<IRoom> Rooms { get; } = rooms;

    public IRepository<ICursor> Cursors { get; } = cursors;

    public IRepository<IWindow> Windows { get; } = windows;

    public void ShowMessage(string message)
    {
        var textBoxScript = (TextBoxScript)scripts[typeof(TextBoxScript).FullName];
        textBoxScript.Show(message);
    }
}
