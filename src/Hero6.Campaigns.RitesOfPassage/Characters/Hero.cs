// <copyright file="Hero.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Localization;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;

public class HeroScript() : Script
{
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
        API.ShowMessage(Strings.HeroLook);
    }

    public void OnGrab()
    {
        API.ShowMessage(Strings.HeroGrab);
    }

    public void OnTalk()
    {
        API.ShowMessage(Strings.HeroTalk);
    }
}
