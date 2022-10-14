// <copyright file="ItemEventAttribute.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.ModuleController.Campaigns.Items
{
    public class ItemEventAttribute : GameEventAttribute
    {
        public ItemEventAttribute(ItemEvent @event)
        {
            Event = @event;
        }

        public ItemEvent Event { get; }
    }
}
