// <copyright file="Item.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Verbs;

namespace LateStartStudio.Hero6.Items;

internal class Item : Composite, IItem
{
    public Item(
        ItemData data,
        IPositionComponent position,
        IVisibleComponent visibility,
        IScriptComponent<ItemEvent> scriptable,
        ISpriteRendererComponent spriteRenderer)
        : base(position, visibility, spriteRenderer, scriptable)
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

    public string Sprite
    {
        get => Get<ISpriteRendererComponent>().Sprite;
        private set => Get<ISpriteRendererComponent>().Sprite = value;
    }

    public bool IsVisible
    {
        get => Get<IVisibleComponent>().IsVisible;
        set => Get<IVisibleComponent>().IsVisible = value;
    }

    public bool Interact(int x, int y, Verb verb)
    {
        if (verb != Verb.Move)
        {
            return Get<IScriptComponent<ItemEvent>>().Invoke(x, y, verb.ToItemEvent());
        }

        return false;
    }
}
