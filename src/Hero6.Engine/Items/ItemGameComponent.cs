// <copyright file="ItemGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;

namespace LateStartStudio.Hero6.Items;

internal class ItemGameComponent : DrawableEntityBase<Item>
{
    public ItemGameComponent(Item entity, MainGame game)
        : base(entity, MonoGame.UpdateOrder.Item, MonoGame.DrawOrder.Item, game)
    {
    }
}
