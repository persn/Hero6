// <copyright file="BentSword.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Localization;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Items;

public class BentSwordScript() : Script
{
    private IItem bentSword;
    private IInventoryItem invBentSword;

    public override void Design(IDesigner designer) => designer
        .Add<IItemDesigner>("Bent Sword")
        .WithSprite("Campaigns/Rites of Albion/Sprites/Items/Bent Sword")
        .OnLook(OnLook)
        .OnGrab(OnGrab)
        .OnTalk(OnTalk)
        ;

    public override void Initialize()
    {
        base.Initialize();
        bentSword = API.Items.BentSword();
        invBentSword = API.InventoryItems.BentSword();
    }

    public void OnLook()
    {
        API.ShowMessage(Strings.BentSwordLook);
    }

    public void OnGrab()
    {
        API.Characters.Player.Room.Remove(bentSword);
        API.ShowMessage(Strings.BentSwordGrab);
        API.Characters.Player.Add(invBentSword);
    }

    public void OnTalk()
    {
        API.ShowMessage(Strings.BentSwordTalk);
    }
}
