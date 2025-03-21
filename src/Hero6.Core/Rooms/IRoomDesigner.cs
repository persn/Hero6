// <copyright file="IRoomDesigner.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Verbs;

namespace LateStartStudio.Hero6.Rooms;

public interface IRoomDesigner : IDesigner
{
    IRoomDesigner WithBackground(string background);

    IRoomDesigner WithWalkAreas(string walkAreasMask);

    IRoomDesigner WithHotspots(string hotspots, Action<IHotspotsDesigner> build);
}

public interface IHotspotDesigner : IInteractionDesigner<IHotspotDesigner>
{
    IHotspotDesigner OnStandingOn(Action interaction) => OnStandingOn(interaction.AsTask);

    IHotspotDesigner OnStandingOn(Func<Task> interaction);
}

public interface IHotspotsDesigner
{
    IHotspotsDesigner For(string color, Action<IHotspotDesigner> build);
}
