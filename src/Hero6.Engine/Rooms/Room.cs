// <copyright file="Room.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;

namespace LateStartStudio.Hero6.Rooms;

internal class Room : Composite, IRoom
{
    private readonly ICampaigns campaigns;
    private readonly List<Item> items = new();
    private readonly List<Character> characters = new();

    public Room(
        RoomData data,
        IVisibleComponent visibleComponent,
        IScriptComponent<HotspotEvent> hotspotScripts,
        ISpriteRendererComponent spriteRendererComponent,
        ICampaigns campaigns)
        : base(visibleComponent, hotspotScripts, spriteRendererComponent)
    {
        Name = data.Name;
        Background = data.Background;
        this.campaigns = campaigns;

        // TODO Should probably DI WalkAreas and Hotspots, or they should be components...
        WalkAreas = new WalkAreas(data.WalkAreasMask);
    }

    public string Name { get; }

    public bool IsVisible
    {
        get => Get<IVisibleComponent>().IsVisible;
        set => Get<IVisibleComponent>().IsVisible = value;
    }

    public override bool Enabled
    {
        get => base.Enabled;
        set => base.Enabled = Get<IScriptComponent<HotspotEvent>>().Enabled = value;
    }

    public string Background
    {
        get => Get<ISpriteRendererComponent>().Sprite;
        private set => Get<ISpriteRendererComponent>().Sprite = value;
    }

    public WalkAreas WalkAreas { get; }

    public IEnumerable<IItem> Items => items;

    public IEnumerable<ICharacter> Characters => characters;

    public void Add(IItem item)
    {
        var i = (Item)item;
        items.Add(i);
        if (campaigns.Current.Player.Room == this)
            i.Enabled = i.IsVisible = true;
    }

    public void Remove(IItem item)
    {
        var i = (Item)item;
        items.Remove(i);
        if (campaigns.Current.Player.Room == this)
            i.Enabled = i.IsVisible = false;
    }

    public void Add(ICharacter character)
    {
        characters.Add((Character)character);
    }

    public void Remove(ICharacter character)
    {
        characters.Remove((Character)character);
    }
}
