// <copyright file="ICharacterAnimationDesigner.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Animations;

public interface ICharacterAnimationDesigner : IDesigner
{
    ICharacterAnimationDesigner WithCenterDownAnimation(string animation);

    ICharacterAnimationDesigner WithCenterUpAnimation(string animation);

    ICharacterAnimationDesigner WithLeftCenterAnimation(string animation);

    ICharacterAnimationDesigner WithLeftDownAnimation(string animation);

    ICharacterAnimationDesigner WithLeftUpAnimation(string animation);

    ICharacterAnimationDesigner WithRightCenterAnimation(string animation);

    ICharacterAnimationDesigner WithRightDownAnimation(string animation);

    ICharacterAnimationDesigner WithRightUpAnimation(string animation);
}
