// <copyright file="Fountain.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Campaigns.RitesOfPassage.Characters;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.ModuleController.Campaigns.Characters;
using LateStartStudio.Hero6.ModuleController.Campaigns.Rooms;
using LateStartStudio.Hero6.ModuleController.Campaigns.Rooms.Regions;
using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Services.Campaigns;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Rooms.Albion;

public class Fountain : RoomModule
{
    public override string Name => "Fountain";

    public override string Background => "Campaigns/Rites of Albion/Rooms/Albion/Fountain/Background";

    public override string WalkAreasMask => "Campaigns/Rites of Albion/Rooms/Albion/Fountain/WalkAreas";

    public override string HotspotsMask => "Campaigns/Rites of Albion/Rooms/Albion/Fountain/Hotspots";
}

public class FountainScript : Script
{
    private readonly ICampaigns campaigns;
    private readonly IUserInterfaces userInterfaces;

    public FountainScript(ICampaigns campaigns, IUserInterfaces userInterfaces)
    {
        this.campaigns = campaigns;
        this.userInterfaces = userInterfaces;
    }

    public override void Initialize()
    {
        base.Initialize();

        var room = campaigns.Current.GetRoom<Fountain>();

        var llewella = campaigns.Current.GetCharacter<Llewella>();
        llewella.X = 200;
        llewella.Y = 150;
        llewella.Face(CharacterDirection.CenterDown);
        room.AddCharacter<Llewella>();
    }

    [HotspotEvent(typeof(Fountain), Hotspots.SouthExit, HotspotEvent.StandingOn)]
    public void OnWhileStandingOnSouthExit(StandingOn e)
    {
        e.Character.ChangeRoom<HillOverAlbion>(220, 190, CharacterDirection.CenterDown);
    }

    [HotspotEvent(typeof(Fountain), Hotspots.Fountain, HotspotEvent.Look)]
    public void OnLookFountain()
    {
        userInterfaces.Current.ShowTextBox(Strings.FountainLook);
    }

    [HotspotEvent(typeof(Fountain), Hotspots.Fountain, HotspotEvent.Grab)]
    public void OnGrabFountain()
    {
        userInterfaces.Current.ShowTextBox(Strings.FountainGrab);
    }

    [HotspotEvent(typeof(Fountain), Hotspots.Fountain, HotspotEvent.Talk)]
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
