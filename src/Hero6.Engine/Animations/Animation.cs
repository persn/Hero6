// <copyright file="Animation.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.Animations;

internal class Animation : Component, IAnimation, ILoadable, LateStartStudio.Hero6.Components.IDrawable
{
    private readonly AnimationData data;
    private readonly MonoGame.Game game;

    private int frame;
    private double elapsed;
    private Rectangle destination;
    private Rectangle source;
    private Texture2D texture;

    public Animation(AnimationData data, MonoGame.Game game)
    {
        this.data = data;
        this.game = game;
    }

    public string Name => data.Name;

    public void Load()
    {
        texture = game.Content.Load<Texture2D>(data.Sprite);
        var position = Parent.Get<IPositionComponent>();
        position.Width = texture.Width / data.Columns;
        position.Height = texture.Height / data.Rows;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);
        var position = Parent.Get<IPositionComponent>();

        if (position == null)
            return;

        elapsed += time.ElapsedGameTime.TotalSeconds;

        if (elapsed >= 1.0f / 16.0f)
        {
            frame++;
            elapsed = 0.0f;
            frame = frame >= data.Columns * data.Rows ? 0 : frame;
        }

        var width = position.Width;
        var height = position.Height;
        var x = position.X;
        var y = position.Y;

        destination = new Rectangle(x, y, width, height);
        var frameCol = width * (frame % data.Columns);
        var frameRow = height * (frame / data.Columns);
        source = new Rectangle(frameCol, frameRow, width, height);
    }

    public void Draw(GameTime gameTime)
    {
        game.SpriteBatch.Draw(texture, destination, source, Color.White);
    }
}
