// <copyright file="HillOverAlbion.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Rooms;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Rooms.Albion;

public class HillOverAlbionScript() : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<IRoomDesigner>("Hill Over Albion")
        .WithBackground("Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/Background")
        .WithWalkAreas("Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/WalkAreas")
        .WithHotspots("Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/Hotspots", designer => designer
            .For(Hotspots.NorthExit, hotspotDesigner => hotspotDesigner
                .OnStandingOn(OnWhileStandingInNorthExit))
            .For(Hotspots.AlbionSign, hotspotDesigner => hotspotDesigner
                .OnLook(OnLookAlbionSign)
                .OnGrab(OnGrabAlbionSign)
                .OnTalk(OnTalkAlbionSign)))
        ;

    public override void Initialize()
    {
        base.Initialize();

        var room = API.Rooms.HillOverAlbion();

        API.Characters.Hero().ChangeRoom(room, 230, 200);

        var bentSword = API.Items.BentSword();
        bentSword.X = 150;
        bentSword.Y = 170;
        bentSword.IsVisible = true;
        room.Add(bentSword);
    }

    public void OnWhileStandingInNorthExit()
    {
        API.Characters.Player.ChangeRoom(API.Rooms.Fountain(), 100, 200, CharacterDirection.CenterUp);
    }

    public void OnLookAlbionSign()
    {
        API.ShowMessage(Strings.AlbionSignLook);
    }

    public void OnGrabAlbionSign()
    {
        API.ShowMessage(Strings.AlbionSignGrab);
    }

    public void OnTalkAlbionSign()
    {
        API.ShowMessage(Strings.AlbionSignTalk);
    }

    internal class Hotspots
    {
        public const string NorthExit = "#FFFFFFFF";
        public const string AlbionSign = "#FFFF0000";
    }
}
