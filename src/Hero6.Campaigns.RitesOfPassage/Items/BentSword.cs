// <copyright file="BentSword.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Items;

public class BentSwordScript : Script
{
    private readonly IUserInterfaces userInterfaces;
    private readonly ICampaigns campaigns;

    private IItem bentSword;
    private IInventoryItem invBentSword;

    public BentSwordScript(IUserInterfaces userInterfaces, ICampaigns campaigns)
    {
        this.userInterfaces = userInterfaces;
        this.campaigns = campaigns;
    }

    public override void Initialize()
    {
        base.Initialize();
        bentSword = API.Items.BentSword;
        invBentSword = API.InventoryItems.BentSword;
    }

    [ScriptEvent<ItemEvent>(nameof(API.Items.BentSword), ItemEvent.Look)]
    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordLook);
    }

    [ScriptEvent<ItemEvent>(nameof(API.Items.BentSword), ItemEvent.Grab)]
    public void OnGrab()
    {
        campaigns.Current.Player.Room.Remove(bentSword);
        userInterfaces.Current.ShowTextBox(Strings.BentSwordGrab);
        campaigns.Current.Player.AddInventoryItem(invBentSword);
    }

    [ScriptEvent<ItemEvent>(nameof(API.Items.BentSword), ItemEvent.Talk)]
    public void OnTalk()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordTalk);
    }
}
