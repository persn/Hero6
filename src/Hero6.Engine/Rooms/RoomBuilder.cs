// <copyright file="RoomBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;
using System.Globalization;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Scripts;
using Microsoft.Extensions.DependencyInjection;

namespace LateStartStudio.Hero6.Rooms;

internal class RoomBuilder : Builder<IRoom, Room>, IRoomBuilder
{
    private readonly RoomData data = new();
    private readonly HotspotsBuilder hotspotsBuilder = new();

    public RoomBuilder(string name, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        data.Name = name;
    }

    public IRoomBuilder WithBackground(string background)
    {
        data.Background = background;
        return this;
    }

    public IRoomBuilder WithWalkAreas(string walkAreasMask)
    {
        data.WalkAreasMask = walkAreasMask;
        return this;
    }

    public IRoomBuilder WithHotspots(string hotspotsMask, Action<IHotspotsBuilder> build)
    {
        data.HotspotsMask = hotspotsMask;
        build?.Invoke(hotspotsBuilder);
        return this;
    }

    protected override object[] BuildParams()
    {
        var visibility = ServiceProvider.CreateInstance<VisibleComponent>();
        var scriptable = ServiceProvider.CreateInstance<HotspotsScriptComponent>(data.HotspotsMask, hotspotsBuilder.Build());
        var spriteRenderer = ServiceProvider.CreateInstance<SpriteRendererComponent>();

        return [data, visibility, scriptable, spriteRenderer];
    }
}

internal class HotspotBuilder : IHotspotBuilder
{
    private readonly Dictionary<HotspotEvent, Func<Task>> interactions = [];

    public IHotspotBuilder OnGrab(Func<Task> interaction)
    {
        interactions[HotspotEvent.Grab] = interaction;
        return this;
    }

    public IHotspotBuilder OnLook(Func<Task> interaction)
    {
        interactions[HotspotEvent.Look] = interaction;
        return this;
    }

    public IHotspotBuilder OnStandingOn(Func<Task> interaction)
    {
        interactions[HotspotEvent.StandingOn] = interaction;
        return this;
    }

    public IHotspotBuilder OnTalk(Func<Task> interaction)
    {
        interactions[HotspotEvent.Talk] = interaction;
        return this;
    }

    internal Dictionary<HotspotEvent, Func<Task>> Build() => interactions;
}

internal class HotspotsBuilder : IHotspotsBuilder
{
    private readonly Dictionary<Color, HotspotBuilder> hotspots = [];

    public IHotspotsBuilder For(string color, Action<IHotspotBuilder> build)
    {
        var c = Color.FromArgb(int.Parse(color.Replace("#", string.Empty), NumberStyles.HexNumber));
        HotspotBuilder builder = new();
        hotspots.Add(c, builder);
        build?.Invoke(builder);
        return this;
    }

    internal Dictionary<(HotspotEvent, Color), Func<Task>> Build()
    {
        Dictionary<(HotspotEvent, Color), Func<Task>> res = [];

        foreach (var colorPair in hotspots)
        {
            foreach (var eventPair in colorPair.Value.Build())
            {
                var @event = eventPair.Key;
                var color = colorPair.Key;

                res[(@event, color)] = eventPair.Value;
            }
        }

        return res;
    }
}
