// <copyright file="UpdateOrder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.MonoGame;

internal enum UpdateOrder
{
    Base,
    Room = Base + 1,
    Character = Room + 1,
    Item = Character + 1,
    Scripts = Item + 1,
}
