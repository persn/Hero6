// <copyright file="WalkAreasGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Rooms;

internal class WalkAreasGameComponent : DrawableGameComponent
{
    private readonly WalkAreas walkAreas;

    public WalkAreasGameComponent(WalkAreas walkAreas, LateStartStudio.Hero6.MonoGame.Game game) : base(game)
    {
        this.walkAreas = walkAreas;
        Visible = false; // We just want to load content, we have no drawing of Walk Areas.. Yet
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        walkAreas.Load(Game.Content.Load<List<WalkArea>>);
    }
}
