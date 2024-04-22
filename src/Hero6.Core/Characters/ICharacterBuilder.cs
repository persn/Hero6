// <copyright file="ICharacterBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;
using LateStartStudio.Hero6.Verbs;

namespace LateStartStudio.Hero6.Characters;

public interface ICharacterBuilder : IBuilder, IInteractionBuilder<ICharacterBuilder>
{
    ICharacterBuilder WithIdleAnimation(string animation);

    ICharacterBuilder WithMoveAnimation(string animation);
}
