// <copyright file="HillOverAlbion.cs" company="Late Start Studio">
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

public class HillOverAlbion : RoomModule
{
    public const string Id = "Hill Over Albion";

    public override string Name => "Hill Over Albion";

    public override string Background => "Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/Background";

    public override string WalkAreasMask => "Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/WalkAreas";

    public override string HotspotsMask => "Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/Hotspots";
}

public class HillOverAlbionScript : Script
{
    private readonly ICampaigns campaigns;
    private readonly IUserInterfaces userInterfaces;

    public HillOverAlbionScript(ICampaigns campaigns, IUserInterfaces userInterfaces)
    {
        this.campaigns = campaigns;
        this.userInterfaces = userInterfaces;
    }

    public override void Initialize()
    {
        base.Initialize();

        var room = campaigns.Current.GetRoom<HillOverAlbion>();

        var hero = campaigns.Current.GetCharacter<Hero>();
        hero.X = 250;
        hero.Y = 220;
        hero.Face(CharacterDirection.CenterDown);
        room.AddCharacter<Hero>();

        var bentSword = API.Items.BentSword;
        bentSword.X = 150;
        bentSword.Y = 170;
        room.Add(bentSword);
    }

    [HotspotEvent(HillOverAlbion.Id, Hotspots.NorthExit, HotspotEvent.StandingOn)]
    public void OnWhileStandingInNorthExit(StandingOn e)
    {
        e.Character.ChangeRoom<Fountain>(100, 200, CharacterDirection.CenterUp);
    }

    [HotspotEvent(HillOverAlbion.Id, Hotspots.AlbionSign, HotspotEvent.Look)]
    public void OnLookAlbionSign()
    {
        userInterfaces.Current.ShowTextBox(Strings.AlbionSignLook);
    }

    [HotspotEvent(HillOverAlbion.Id, Hotspots.AlbionSign, HotspotEvent.Grab)]
    public void OnGrabAlbionSign()
    {
        userInterfaces.Current.ShowTextBox(Strings.AlbionSignGrab);
    }

    [HotspotEvent(HillOverAlbion.Id, Hotspots.AlbionSign, HotspotEvent.Talk)]
    public void OnTalkAlbionSign()
    {
        userInterfaces.Current.ShowTextBox(Strings.AlbionSignTalk);
    }

    internal class Hotspots
    {
        public const string NorthExit = "#FFFFFFFF";
        public const string AlbionSign = "#FFFF0000";
    }
}
