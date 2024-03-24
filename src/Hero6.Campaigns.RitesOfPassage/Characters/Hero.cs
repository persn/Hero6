// <copyright file="Hero.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;

public class HeroScript : Script
{
    private readonly IUserInterfaces userInterfaces;

    public HeroScript(IUserInterfaces userInterfaces)
    {
        this.userInterfaces = userInterfaces;
    }

    [ScriptEvent<CharacterEvent>(API.Characters.HeroName, CharacterEvent.Look)]
    public void OnLook()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroLook);
    }

    [ScriptEvent<CharacterEvent>(API.Characters.HeroName, CharacterEvent.Grab)]
    public void OnGrab()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroGrab);
    }

    [ScriptEvent<CharacterEvent>(API.Characters.HeroName, CharacterEvent.Talk)]
    public void OnTalk()
    {
        userInterfaces.Current.ShowTextBox(Strings.HeroTalk);
    }
}
