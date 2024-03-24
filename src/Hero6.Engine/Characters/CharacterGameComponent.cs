// <copyright file="CharacterGameComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.MonoGame;

namespace LateStartStudio.Hero6.Characters;

internal class CharacterGameComponent : DrawableEntityBase<Character>
{
    public CharacterGameComponent(Character entity, LateStartStudio.Hero6.MonoGame.Game game)
        : base(entity, MonoGame.UpdateOrder.Character, MonoGame.DrawOrder.Character, game)
    {
    }
}
