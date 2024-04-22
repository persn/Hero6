// <copyright file="IRoomBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Verbs;

namespace LateStartStudio.Hero6.Rooms;

public interface IRoomBuilder : IBuilder
{
    IRoomBuilder WithBackground(string background);

    IRoomBuilder WithWalkAreas(string walkAreasMask);

    IRoomBuilder WithHotspots(string hotspotsMask, Action<IHotspotsBuilder> build);
}

public interface IHotspotBuilder : IBuilder, IInteractionBuilder<IHotspotBuilder>
{
    IHotspotBuilder OnStandingOn(Action interaction) => OnStandingOn(interaction.AsTask);

    IHotspotBuilder OnStandingOn(Func<Task> interaction);
}

public interface IHotspotsBuilder
{
    IHotspotsBuilder For(string color, Action<IHotspotBuilder> build);
}
