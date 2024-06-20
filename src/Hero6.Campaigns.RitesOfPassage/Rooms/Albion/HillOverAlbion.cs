// <copyright file="HillOverAlbion.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Localization;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.Services.UserInterfaces;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Rooms.Albion;

public class HillOverAlbionScript(IUserInterfaces userInterfaces) : Script
{
    private readonly IUserInterfaces userInterfaces = userInterfaces;

    public override void Design(IDesigner designer) => designer
        .Add<IRoomBuilder>("Hill Over Albion", builder => builder
            .WithBackground("Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/Background")
            .WithWalkAreas("Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/WalkAreas")
            .WithHotspots("Campaigns/Rites of Albion/Rooms/Albion/Hill Over Albion/Hotspots", builder => builder
                .For(Hotspots.NorthExit, builder => builder
                    .OnStandingOn(OnWhileStandingInNorthExit))
                .For(Hotspots.AlbionSign, builder => builder
                    .OnLook(OnLookAlbionSign)
                    .OnGrab(OnGrabAlbionSign)
                    .OnTalk(OnTalkAlbionSign))))
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
        userInterfaces.Current.ShowTextBox(Strings.AlbionSignLook);
    }

    public void OnGrabAlbionSign()
    {
        userInterfaces.Current.ShowTextBox(Strings.AlbionSignGrab);
    }

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
