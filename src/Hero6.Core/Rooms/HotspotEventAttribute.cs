// <copyright file="HotspotEventAttribute.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Rooms;

/// <inheritdoc />
[AttributeUsage(AttributeTargets.Method)]
public class HotspotEventAttribute : ScriptEventAttribute<HotspotEvent>
{
    public HotspotEventAttribute(string name, string id, HotspotEvent @event) : base(name, @event)
    {
        Id = id;
    }

    public string Id { get; }
}
