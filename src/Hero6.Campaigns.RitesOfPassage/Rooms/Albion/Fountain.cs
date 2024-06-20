// <copyright file="Fountain.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Rooms.Albion;

public class FountainScript(IUserInterfaces userInterfaces) : Script
{
    private readonly IUserInterfaces userInterfaces = userInterfaces;

    public override void Design(IDesigner designer) => designer
        .Add<IRoomBuilder>("Fountain", builder => builder
            .WithBackground("Campaigns/Rites of Albion/Rooms/Albion/Fountain/Background")
            .WithWalkAreas("Campaigns/Rites of Albion/Rooms/Albion/Fountain/WalkAreas")
            .WithHotspots("Campaigns/Rites of Albion/Rooms/Albion/Fountain/Hotspots", builder => builder
                .For(Hotspots.SouthExit, builder => builder
                    .OnStandingOn(OnWhileStandingOnSouthExit))
                .For(Hotspots.Fountain, builder => builder
                    .OnLook(OnLookFountain)
                    .OnGrab(OnGrabFountain)
                    .OnTalk(OnTalkFountain))))
        ;

    public override void Initialize()
    {
        base.Initialize();

        API.Characters.Llewella().ChangeRoom(API.Rooms.Fountain(), 200, 150);
    }

    public void OnWhileStandingOnSouthExit()
    {
        API.Characters.Player.ChangeRoom(API.Rooms.HillOverAlbion(), 220, 190, CharacterDirection.CenterDown);
    }

    public void OnLookFountain()
    {
        userInterfaces.Current.ShowTextBox(Strings.FountainLook);
    }

    public void OnGrabFountain()
    {
        userInterfaces.Current.ShowTextBox(Strings.FountainGrab);
    }

    public void OnTalkFountain()
    {
        userInterfaces.Current.ShowTextBox(Strings.FountainTalk);
    }

    internal class Hotspots
    {
        public const string SouthExit = "#FFFFFFFF";
        public const string Fountain = "#FFFF0000";
    }
}
