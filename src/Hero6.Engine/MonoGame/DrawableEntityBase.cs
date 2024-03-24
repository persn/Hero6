// <copyright file="DrawableEntityBase.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.MonoGame;

internal class DrawableEntityBase<T> : DrawableGameComponent where T : Composite
{
    protected DrawableEntityBase(T entity, UpdateOrder updateOrder, DrawOrder drawOrder, Game game) : base(game)
    {
        Entity = entity;
        UpdateOrder = (int)updateOrder;
        DrawOrder = (int)drawOrder;
        Game = game;
        Enabled = entity.Enabled;
        Visible = false;
        entity.EnabledChanged += (s, e) => Enabled = entity.Enabled;

        foreach (var c in entity.Components)
        {
            if (c is IVisibleComponent visibility)
            {
                Visible = visibility.IsVisible;
                visibility.IsVisibleChanged += (s, e) => Visible = visibility.IsVisible;
            }
        }
    }

    protected T Entity { get; }

    protected new Game Game { get; }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        Entity.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        Entity.Draw(gameTime);
    }

    public override void Initialize()
    {
        Entity.Initialize();
        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        Entity.Load();
    }
}
