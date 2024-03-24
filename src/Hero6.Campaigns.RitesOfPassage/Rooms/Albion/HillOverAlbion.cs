// <copyright file="HillOverAlbion.cs" company="Late Start Studio">
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

public class HillOverAlbionScript : Script
{
    private readonly IUserInterfaces userInterfaces;

    public HillOverAlbionScript(IUserInterfaces userInterfaces)
    {
        this.userInterfaces = userInterfaces;
    }

    public override void Initialize()
    {
        base.Initialize();

        var room = API.Rooms.HillOverAlbion;

        API.Characters.Hero.ChangeRoom(room, 230, 200);

        var bentSword = API.Items.BentSword;
        bentSword.X = 150;
        bentSword.Y = 170;
        bentSword.IsVisible = true;
        room.Add(bentSword);
    }

    [HotspotEvent(API.Rooms.HillOverAlbionName, Hotspots.NorthExit, HotspotEvent.StandingOn)]
    public void OnWhileStandingInNorthExit(StandingOn e)
    {
        e.Character.ChangeRoom(API.Rooms.Fountain, 100, 200, CharacterDirection.CenterUp);
    }

    [HotspotEvent(API.Rooms.HillOverAlbionName, Hotspots.AlbionSign, HotspotEvent.Look)]
    public void OnLookAlbionSign()
    {
        userInterfaces.Current.ShowTextBox(Strings.AlbionSignLook);
    }

    [HotspotEvent(API.Rooms.HillOverAlbionName, Hotspots.AlbionSign, HotspotEvent.Grab)]
    public void OnGrabAlbionSign()
    {
        userInterfaces.Current.ShowTextBox(Strings.AlbionSignGrab);
    }

    [HotspotEvent(API.Rooms.HillOverAlbionName, Hotspots.AlbionSign, HotspotEvent.Talk)]
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
