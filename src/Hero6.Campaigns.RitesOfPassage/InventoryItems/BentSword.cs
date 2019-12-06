// <copyright file="BentSword.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.ModuleController.Campaigns.InventoryItems;
using LateStartStudio.Hero6.Services.DependencyInjection;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.InventoryItems
{
    [Injectable]
    public sealed class BentSword : InventoryItemModule
    {
        public override string Name => "Bent Sword";
    }
}
