// <copyright file="Item.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.ModuleController.Campaigns;
using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Items;

internal class Item : Composite, IItem
{
    public Item(
        ItemData data,
        IPositionComponent position,
        IVisibleComponent visibility,
        IScriptComponent<ItemEvent> scriptable)
        : base(position, visibility, scriptable)
    {
        Name = data.Name;
        Sprite = data.Sprite;
    }

    public string Name { get; }

    public int X
    {
        get => Get<IPositionComponent>().X;
        set => Get<IPositionComponent>().X = value;
    }

    public int Y
    {
        get => Get<IPositionComponent>().Y;
        set => Get<IPositionComponent>().Y = value;
    }

    public string Sprite { get; }

    public bool IsVisible
    {
        get => Get<IVisibleComponent>().IsVisible;
        set => Get<IVisibleComponent>().IsVisible = value;
    }

    public bool Interact(int x, int y, Interaction interaction)
    {
        if (Get<IPositionComponent>().Intersects(x, y) && interaction != Interaction.Move)
        {
            Get<IScriptComponent<ItemEvent>>().Invoke(interaction.ToItemEvent());
        }

        return false;
    }
}
