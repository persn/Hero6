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

public class FountainScript : Script
{
    private readonly IUserInterfaces userInterfaces;

    public FountainScript(IUserInterfaces userInterfaces)
    {
        this.userInterfaces = userInterfaces;
    }

    public override void Initialize()
    {
        base.Initialize();

        API.Characters.Llewella.ChangeRoom(API.Rooms.Fountain, 200, 150);
    }

    [HotspotEvent(API.Rooms.FountainName, Hotspots.SouthExit, HotspotEvent.StandingOn)]
    public void OnWhileStandingOnSouthExit(StandingOn e)
    {
        e.Character.ChangeRoom(API.Rooms.HillOverAlbion, 220, 190, CharacterDirection.CenterDown);
    }

    [HotspotEvent(API.Rooms.FountainName, Hotspots.Fountain, HotspotEvent.Look)]
    public void OnLookFountain()
    {
        userInterfaces.Current.ShowTextBox(Strings.FountainLook);
    }

    [HotspotEvent(API.Rooms.FountainName, Hotspots.Fountain, HotspotEvent.Grab)]
    public void OnGrabFountain()
    {
        userInterfaces.Current.ShowTextBox(Strings.FountainGrab);
    }

    [HotspotEvent(API.Rooms.FountainName, Hotspots.Fountain, HotspotEvent.Talk)]
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
