// <copyright file="CharacterGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Characters;

internal class CharacterGameComponent(MainGame game, IRepository<ICharacter> characters, IServiceProvider serviceProvider, IGameSettings gameSettings)
    : DrawableEntityBase<ICharacter>(MonoGame.UpdateOrder.Character, MonoGame.DrawOrder.Character, game, characters, serviceProvider)
{
    private readonly IGameSettings gameSettings = gameSettings;

    public override void Update(GameTime gameTime)
    {
        if (!gameSettings.IsPaused)
            base.Update(gameTime);
    }
}
