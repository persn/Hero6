// <copyright file="HotspotsGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Scripts;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Rooms;

internal class HotspotsGameComponent : DrawableGameComponent
{
    private readonly IGameSettings gameSettings;
    private readonly HotspotsScriptComponent[] hotspots;

    public HotspotsGameComponent(IGameSettings gameSettings, IRepository<IRoom> rooms, MainGame game) : base(game)
    {
        this.gameSettings = gameSettings;
        hotspots = rooms
            .Cast<Room>()
            .Select(r => r.Get<IScriptComponent<HotspotEvent>>())
            .Cast<HotspotsScriptComponent>()
            .ToArray();
        Visible = false; // We just want to load content, we have no drawing of Hotspots.. Yet
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        foreach (var hotspot in hotspots)
            hotspot.Load(Game.Content.Load<Texture2D>);
    }
}
