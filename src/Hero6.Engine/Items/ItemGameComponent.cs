// <copyright file="ItemGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Items;

internal class ItemGameComponent(MainGame game, IRepository<IItem> items, IServiceProvider serviceProvider, IGameSettings gameSettings)
    : DrawableEntityBase<IItem>(MonoGame.UpdateOrder.Item, MonoGame.DrawOrder.Item, game, items, serviceProvider)
{
    private readonly IGameSettings gameSettings = gameSettings;

    public override void Update(GameTime gameTime)
    {
        if (!gameSettings.IsPaused)
            base.Update(gameTime);
    }
}
