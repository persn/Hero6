// <copyright file="BentSword.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.InventoryItems;

internal class BentSwordScript : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<IInventoryItemBuilder>("Bent Sword", null)
        ;
}
