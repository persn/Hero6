// <copyright file="Fountain.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Rooms;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Rooms.Albion;

public class FountainScript() : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<IRoomDesigner>("Fountain")
        .WithBackground("Campaigns/Rites of Albion/Rooms/Albion/Fountain/Background")
        .WithWalkAreas("Campaigns/Rites of Albion/Rooms/Albion/Fountain/WalkAreas")
        .WithHotspots("Campaigns/Rites of Albion/Rooms/Albion/Fountain/Hotspots", designer => designer
            .For(Hotspots.SouthExit, hotspotDesigner => hotspotDesigner
                .OnStandingOn(OnWhileStandingOnSouthExit))
            .For(Hotspots.Fountain, hotspotDesigner => hotspotDesigner
                .OnLook(OnLookFountain)
                .OnGrab(OnGrabFountain)
                .OnTalk(OnTalkFountain)))
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
        API.ShowMessage(Strings.FountainLook);
    }

    public void OnGrabFountain()
    {
        API.ShowMessage(Strings.FountainGrab);
    }

    public void OnTalkFountain()
    {
        API.ShowMessage(Strings.FountainTalk);
    }

    internal class Hotspots
    {
        public const string SouthExit = "#FFFFFFFF";
        public const string Fountain = "#FFFF0000";
    }
}
