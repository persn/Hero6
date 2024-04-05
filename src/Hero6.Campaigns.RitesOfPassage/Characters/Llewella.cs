// <copyright file="Llewella.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;

public class LlewellaScript : Script
{
    private readonly IUserInterfaces userInterfaces;
    private readonly ICampaigns campaigns;

    private ICharacter llewella;
    private IInventoryItem bentSword;

    public LlewellaScript(IUserInterfaces userInterfaces, ICampaigns campaigns)
    {
        this.userInterfaces = userInterfaces;
        this.campaigns = campaigns;
    }

    public override void Initialize()
    {
        base.Initialize();
        llewella = API.Characters.Llewella;
        bentSword = API.InventoryItems.BentSword;
    }

    [ScriptEvent<CharacterEvent>(API.Characters.LlewellaName, CharacterEvent.Look)]
    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.LlewellaLook);
    }

    [ScriptEvent<CharacterEvent>(API.Characters.LlewellaName, CharacterEvent.Grab)]
    public void OnGrab()
    {
        userInterfaces.Current.ShowTextBox(Strings.LlewellaGrab);
    }

    [ScriptEvent<CharacterEvent>(API.Characters.LlewellaName, CharacterEvent.Talk)]
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
