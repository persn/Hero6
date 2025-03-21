// <copyright file="Llewella.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;

public class LlewellaScript(IUserInterfaces userInterfaces) : Script
{
    private readonly IUserInterfaces userInterfaces = userInterfaces;

    private ICharacter llewella;
    private IInventoryItem bentSword;

    public override void Design(IDesigner designer) => designer
        .Add<ICharacterDesigner>("Llewella")
        .WithIdleAnimation("Llewella Idle")
        .OnLook(OnLook)
        .OnGrab(OnGrab)
        .OnTalk(OnTalk)
        ;

    public override void Initialize()
    {
        base.Initialize();
        llewella = API.Characters.Llewella();
        bentSword = API.InventoryItems.BentSword();
    }

    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.LlewellaLook);
    }

    public void OnGrab()
    {
        userInterfaces.Current.ShowTextBox(Strings.LlewellaGrab);
    }

    public void OnTalk()
    {
        if (llewella.Has(bentSword))
        {
            userInterfaces.Current.ShowTextBox(Strings.LlewellaTalk3);
        }
        else if (API.Characters.Player.Has(bentSword))
        {
            userInterfaces.Current.ShowTextBox(Strings.LlewellaTalk1);
            llewella.Add(bentSword);
            API.Characters.Player.Remove(bentSword);
        }
        else
        {
            userInterfaces.Current.ShowTextBox(Strings.LlewellaTalk2);
        }
    }
}
