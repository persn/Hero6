// <copyright file="BentSword.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Items;

public class BentSwordScript(IUserInterfaces userInterfaces) : Script
{
    private readonly IUserInterfaces userInterfaces = userInterfaces;

    private IItem bentSword;
    private IInventoryItem invBentSword;

    public override void Design(IDesigner designer) => designer
        .Add<IItemBuilder>("Bent Sword", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Sprites/Items/Bent Sword")
            .OnLook(OnLook)
            .OnGrab(OnGrab)
            .OnTalk(OnTalk))
        ;

    public override void Initialize()
    {
        base.Initialize();
        bentSword = API.Items.BentSword();
        invBentSword = API.InventoryItems.BentSword();
    }

    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordLook);
    }

    public void OnGrab()
    {
        API.Characters.Player.Room.Remove(bentSword);
        userInterfaces.Current.ShowTextBox(Strings.BentSwordGrab);
        API.Characters.Player.Add(invBentSword);
    }

    public void OnTalk()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordTalk);
    }
}
