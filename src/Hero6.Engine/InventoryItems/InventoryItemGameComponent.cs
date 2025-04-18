// <copyright file="InventoryItemGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.InventoryItems;

internal class InventoryItemGameComponent(InventoryItem inventoryItem, IGameSettings gameSettings, MainGame game) : DrawableGameComponent(game)
{
    private readonly InventoryItem inventoryItem = inventoryItem;
    private readonly IGameSettings gameSettings = gameSettings;

    public override void Update(GameTime gameTime)
    {
        if (!gameSettings.IsPaused)
        {
            base.Update(gameTime);
            inventoryItem.Update(gameTime);
        }
    }

    public override void Initialize()
    {
        base.Initialize();
        inventoryItem.Initialize();
    }
}
