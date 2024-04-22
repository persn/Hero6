// <copyright file="IAnimationBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Animations;

public interface IAnimationBuilder : IBuilder
{
    IAnimationBuilder WithSprite(string sprite);

    IAnimationBuilder WithColumns(int columns);

    IAnimationBuilder WithRows(int rows);
}
