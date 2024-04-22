// <copyright file="ICharacterAnimationBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Animations;

public interface ICharacterAnimationBuilder : IBuilder
{
    ICharacterAnimationBuilder WithCenterDownAnimation(string animation);

    ICharacterAnimationBuilder WithCenterUpAnimation(string animation);

    ICharacterAnimationBuilder WithLeftCenterAnimation(string animation);

    ICharacterAnimationBuilder WithLeftDownAnimation(string animation);

    ICharacterAnimationBuilder WithLeftUpAnimation(string animation);

    ICharacterAnimationBuilder WithRightCenterAnimation(string animation);

    ICharacterAnimationBuilder WithRightDownAnimation(string animation);

    ICharacterAnimationBuilder WithRightUpAnimation(string animation);
}
