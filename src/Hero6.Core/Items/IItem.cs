// <copyright file="IItem.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Items;

/// <summary>
/// Items that can be placed, seen, and interacted with in a room.
/// </summary>
public interface IItem : INameable
{
    /// <summary>
    /// Gets or sets the X coordinate.
    /// </summary>
    int X { get; set; }

    /// <summary>
    /// Gets or sets the Y coordinate.
    /// </summary>
    int Y { get; set; }

    /// <summary>
    /// Gets the item sprite.
    /// </summary>
    string Sprite { get; }

    /// <summary>
    /// Gets or sets the item visibility.
    /// </summary>
    bool IsVisible { get; set; }
}
