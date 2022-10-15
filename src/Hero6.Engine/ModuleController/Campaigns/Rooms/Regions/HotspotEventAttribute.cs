// <copyright file="HotspotEventAttribute.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.ModuleController.Campaigns.Rooms.Regions
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Method)]
    public class HotspotEventAttribute : GameEventAttribute
    {
        public HotspotEventAttribute(string id, HotspotEvent @event)
        {
            ID = id;
            Event = @event;
        }

        public string ID { get; }

        public HotspotEvent Event { get; }
    }
}
