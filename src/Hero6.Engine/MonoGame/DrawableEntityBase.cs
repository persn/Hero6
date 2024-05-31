// <copyright file="DrawableEntityBase.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace LateStartStudio.Hero6.MonoGame;

internal class DrawableEntityBase<T> : DrawableGameComponent
    where T : INameable
{
    private readonly Composite[] entities;
    private readonly IServiceScope serviceScope;

    private SpriteBatch spriteBatch;

    protected DrawableEntityBase(UpdateOrder updateOrder, DrawOrder drawOrder, MainGame game, IRepository<T> entities, IServiceProvider serviceProvider) : base(game)
    {
        UpdateOrder = (int)updateOrder;
        DrawOrder = (int)drawOrder;
        Game = game;
        this.entities = entities.GetAll().Cast<Composite>().ToArray();

        serviceScope = serviceProvider.CreateScope();
        foreach (var entity in this.entities)
            entity.ServiceScope = serviceScope;
    }

    protected new MainGame Game { get; }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);

        foreach (var entity in entities.Where(e => e.Enabled))
            entity.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);

        spriteBatch.Begin(SpriteSortMode.Deferred, transformMatrix: Game.TransformMatrix);

        foreach (var entity in entities.Where(e => e.Enabled))
            entity.Draw(gameTime);

        spriteBatch.End();
    }

    public override void Initialize()
    {
        foreach (var entity in entities)
            entity.Initialize();

        base.Initialize();
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        spriteBatch = serviceScope.ServiceProvider.GetRequiredService<SpriteBatch>();

        foreach (var entity in entities)
            entity.Load();
    }
}
