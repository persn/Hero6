// <copyright file="ItemBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Items;

internal class ItemBuilder : Builder<IItem, Item>, IItemBuilder
{
    private readonly ItemData data = new();
    private readonly Dictionary<ItemEvent, Func<Task>> interactions = [];

    public ItemBuilder(string name, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        data.Name = name;
    }

    public IItemBuilder WithSprite(string sprite)
    {
        data.Sprite = sprite;
        return this;
    }

    public IItemBuilder OnLook(Func<Task> interaction)
    {
        interactions[ItemEvent.Look] = interaction;
        return this;
    }

    public IItemBuilder OnGrab(Func<Task> interaction)
    {
        interactions[ItemEvent.Grab] = interaction;
        return this;
    }

    public IItemBuilder OnTalk(Func<Task> interaction)
    {
        interactions[ItemEvent.Talk] = interaction;
        return this;
    }


    protected override object[] BuildParams()
    {
        var position = ServiceProvider.CreateInstance<SystemDrawingPositionComponent>();
        var visibility = ServiceProvider.CreateInstance<VisibleComponent>();
        var scriptable = ServiceProvider.CreateInstance<ScriptComponent<ItemEvent>>(interactions);
        var spriteRenderer = ServiceProvider.CreateInstance<SpriteRendererComponent>();

        return [data, position, visibility, scriptable, spriteRenderer];
    }
}
