// <copyright file="ItemGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Items;

internal class ItemGameComponent : DrawableGameComponent
{
    private readonly Item item;
    private readonly MonoGame.Game game;

    private Texture2D sprite;
    private Vector2 position;

    public ItemGameComponent(Item item, MonoGame.Game game) : base(game)
    {
        Visible = item.IsVisible;
        Enabled = item.Enabled;
        this.item = item;
        item.EnabledChanged += (s, e) => Enabled = item.Enabled;
        item.Get<IVisibleComponent>().IsVisibleChanged += (s, e) => Visible = item.IsVisible;
        item.Get<IPositionComponent>().PositionChanged += (s, e) => position = new(item.X, item.Y);
        this.game = game;
        DrawOrder = (int)LateStartStudio.Hero6.MonoGame.DrawOrder.Item;
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        item.Update();
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        game.SpriteBatch.Draw(sprite, position, Color.White);
    }

    public override void Initialize()
    {
        base.Initialize();
        item.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();

        sprite = game.Content.Load<Texture2D>(item.Sprite);
        item.Get<IPositionComponent>().Width = sprite.Width;
        item.Get<IPositionComponent>().Height = sprite.Height;
    }
}
