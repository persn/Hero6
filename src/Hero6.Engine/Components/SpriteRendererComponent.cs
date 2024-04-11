// <copyright file="SpriteRendererComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Components;

internal interface ISpriteRendererComponent : IComponent
{
    string Sprite { get; set; }
}

internal class SpriteRendererComponent : Component, ISpriteRendererComponent, ILoadable, IDrawable
{
    private readonly MainGame game;

    private Texture2D sprite;
    private Vector2 position;

    public SpriteRendererComponent(MainGame game)
    {
        this.game = game;
    }

    public string Sprite { get; set; }

    public override void Initialize()
    {
        base.Initialize();

        var positionComponent = Parent.Get<IPositionComponent>();
        if (positionComponent != null)
            positionComponent.PositionChanged += (s, e) => position = new(positionComponent.X, positionComponent.Y);
    }

    public void Load()
    {
        sprite = game.Content.Load<Texture2D>(Sprite);

        var positionComponent = Parent.Get<IPositionComponent>();
        if (positionComponent != null)
        {
            Parent.Get<IPositionComponent>().Width = sprite.Width;
            Parent.Get<IPositionComponent>().Height = sprite.Height;
        }
    }

    public void Draw(GameTime gameTime)
    {
        game.SpriteBatch.Draw(sprite, position, Color.White);
    }
}
