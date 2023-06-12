// <copyright file="Llewella.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Campaigns.RitesOfPassage.Animations.Characters.Llewella.Idle;
using LateStartStudio.Hero6.Campaigns.RitesOfPassage.InventoryItems;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.ModuleController.Campaigns.Characters;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;

public class Llewella : CharacterModule
{
    public const string Id = "Llewella";

    public override string Name => Id;
}

public class LlewellaScript : Script
{
    private readonly IUserInterfaces userInterfaces;
    private readonly ICampaigns campaigns;

    private ICharacterModule llewella;

    public LlewellaScript(IUserInterfaces userInterfaces, ICampaigns campaigns)
    {
        this.userInterfaces = userInterfaces;
        this.campaigns = campaigns;
    }

    public override void Initialize()
    {
        base.Initialize();
        llewella = campaigns.Current.GetCharacter<Llewella>();
        llewella.IdleAnimation = campaigns.Current.GetCharacterAnimation<LlewellaIdle>();
    }

    [CharacterEvent(Llewella.Id, CharacterEvent.Look)]
    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.LlewellaLook);
    }

    [CharacterEvent(Llewella.Id, CharacterEvent.Grab)]
    public void OnGrab()
    {
        userInterfaces.Current.ShowTextBox(Strings.LlewellaGrab);
    }

    [CharacterEvent(Llewella.Id, CharacterEvent.Talk)]
    public void OnTalk()
    {
        if (llewella.HasInventoryItem<BentSword>())
        {
            userInterfaces.Current.ShowTextBox(Strings.LlewellaTalk3);
        }
        else if (campaigns.Current.Player.HasInventoryItem<BentSword>())
        {
            userInterfaces.Current.ShowTextBox(Strings.LlewellaTalk1);
            llewella.AddInventoryItem<BentSword>();
            campaigns.Current.Player.RemoveInventoryItem<BentSword>();
        }
        else
        {
            userInterfaces.Current.ShowTextBox(Strings.LlewellaTalk2);
        }
    }
}
