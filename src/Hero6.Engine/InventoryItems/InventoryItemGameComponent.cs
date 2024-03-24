// <copyright file="InventoryItemGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.InventoryItems;

internal class InventoryItemGameComponent : DrawableGameComponent
{
    private readonly InventoryItem inventoryItem;

    public InventoryItemGameComponent(InventoryItem inventoryItem, LateStartStudio.Hero6.MonoGame.Game game) : base(game)
    {
        this.inventoryItem = inventoryItem;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        inventoryItem.Update(gameTime);
    }

    public override void Initialize()
    {
        base.Initialize();
        inventoryItem.Initialize();
    }
}
