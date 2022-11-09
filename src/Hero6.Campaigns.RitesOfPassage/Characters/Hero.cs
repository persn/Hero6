// <copyright file="Hero.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Campaigns.RitesOfPassage.Animations.Characters.Hero.Idle;
using LateStartStudio.Hero6.Campaigns.RitesOfPassage.Animations.Characters.Hero.Walk;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.ModuleController.Campaigns.Characters;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;

public sealed class Hero : CharacterModule
{
    public override string Name => "Hero";
}

public class HeroScript : Script
{
    private readonly IUserInterfaces userInterfaces;
    private readonly ICampaigns campaigns;

    public HeroScript(IUserInterfaces userInterfaces, ICampaigns campaigns)
    {
        this.userInterfaces = userInterfaces;
        this.campaigns = campaigns;
    }

    public override void Initialize()
    {
        base.Initialize();
        var hero = campaigns.Current.GetCharacter<Hero>();
        hero.IdleAnimation = campaigns.Current.GetCharacterAnimation<HeroIdle>();
        hero.MoveAnimation = campaigns.Current.GetCharacterAnimation<HeroWalk>();
    }

    [CharacterEvent(typeof(Hero), CharacterEvent.Look)]
    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroLook);
    }

    [CharacterEvent(typeof(Hero), CharacterEvent.Grab)]
    public void OnGrab()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroGrab);
    }

    [CharacterEvent(typeof(Hero), CharacterEvent.Talk)]
    public void OnTalk()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroTalk);
    }
}
