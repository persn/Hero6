// <copyright file="WalkAreasGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Rooms;

internal class WalkAreasGameComponent : DrawableGameComponent
{
    private readonly IGameSettings gameSettings;
    private readonly WalkAreas[] walkAreas;

    public WalkAreasGameComponent(IGameSettings gameSettings, IRepository<IRoom> rooms, MainGame game) : base(game)
    {
        this.gameSettings = gameSettings;
        walkAreas = rooms.Cast<Room>().Select(r => r.WalkAreas).ToArray();
        Visible = false; // We just want to load content, we have no drawing of Walk Areas.. Yet
    }

    public override void Update(GameTime gameTime)
    {
        if (!gameSettings.IsPaused)
            base.Update(gameTime);
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        foreach (var walkArea in walkAreas)
            walkArea.Load(Game.Content.Load<List<WalkArea>>);
    }
}
