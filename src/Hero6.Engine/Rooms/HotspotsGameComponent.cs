// <copyright file="HotspotsGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Rooms;

internal class HotspotsGameComponent : DrawableGameComponent
{
    private readonly HotspotsScriptComponent hotspots;

    public HotspotsGameComponent(HotspotsScriptComponent hotspots, MainGame game) : base(game)
    {
        this.hotspots = hotspots;
        Visible = false; // We just want to load content, we have no drawing of Hotspots.. Yet
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        hotspots.Load(Game.Content.Load<Texture2D>);
    }
}
