// <copyright file="IAnimationDesigner.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Animations;

public interface IAnimationDesigner : IDesigner
{
    IAnimationDesigner WithSprite(string sprite);

    IAnimationDesigner WithColumns(int columns);

    IAnimationDesigner WithRows(int rows);
}
