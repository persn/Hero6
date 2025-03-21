// <copyright file="Hero.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;

public class HeroScript(IUserInterfaces userInterfaces) : Script
{
    private readonly IUserInterfaces userInterfaces = userInterfaces;

    public override void Design(IDesigner designer) => designer
        .Add<ICharacterDesigner>("Hero")
        .WithIdleAnimation("Hero Idle")
        .WithMoveAnimation("Hero Walk")
        .OnLook(OnLook)
        .OnGrab(OnGrab)
        .OnTalk(OnTalk)
        ;

    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroLook);
    }

    public void OnGrab()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroGrab);
    }

    public void OnTalk()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroTalk);
    }
}
