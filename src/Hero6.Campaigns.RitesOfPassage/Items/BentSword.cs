// <copyright file="BentSword.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Items;

public class BentSwordScript : Script
{
    private readonly IUserInterfaces userInterfaces;

    private IItem bentSword;
    private IInventoryItem invBentSword;

    public BentSwordScript(IUserInterfaces userInterfaces)
    {
        this.userInterfaces = userInterfaces;
    }

    public override void Initialize()
    {
        base.Initialize();
        bentSword = API.Items.BentSword;
        invBentSword = API.InventoryItems.BentSword;
    }

    [ScriptEvent<ItemEvent>(API.Items.BentSwordName, ItemEvent.Look)]
    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordLook);
    }

    [ScriptEvent<ItemEvent>(API.Items.BentSwordName, ItemEvent.Grab)]
    public void OnGrab()
    {
        API.Characters.Player.Room.Remove(bentSword);
        userInterfaces.Current.ShowTextBox(Strings.BentSwordGrab);
        API.Characters.Player.Add(invBentSword);
    }

    [ScriptEvent<ItemEvent>(API.Items.BentSwordName, ItemEvent.Talk)]
    public void OnTalk()
    {
        userInterfaces.Current.ShowTextBox(Strings.BentSwordTalk);
    }
}
