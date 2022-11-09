// <copyright file="BentSword.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.ModuleController.Campaigns.Items;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Items;

public class BentSword : ItemModule
{
    public override string Name => "Bent Sword";

    public override string Sprite => "Campaigns/Rites of Albion/Sprites/Items/Bent Sword";
}

public class BentSwordScript : Script
{
    private readonly IUserInterfaces userInterfaces;
    private readonly ICampaigns campaigns;

    private IItemModule bentSword;

    public BentSwordScript(IUserInterfaces userInterfaces, ICampaigns campaigns)
    {
        this.userInterfaces = userInterfaces;
        this.campaigns = campaigns;
    }

    public override void Initialize()
    {
        base.Initialize();
        bentSword = campaigns.Current.GetItem<BentSword>();
    }

    [ItemEvent(typeof(BentSword), ItemEvent.Look)]
    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordLook);
    }

    [ItemEvent(typeof(BentSword), ItemEvent.Grab)]
    public void OnGrab()
    {
        bentSword.IsVisible = false;
        userInterfaces.Current.ShowTextBox(Strings.BentSwordGrab);
        campaigns.Current.Player.AddInventoryItem<InventoryItems.BentSword>();
    }

    [ItemEvent(typeof(BentSword), ItemEvent.Talk)]
    public void OnTalk()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordTalk);
    }
}
