// <copyright file="IRoom.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Items;
using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Rooms;

public interface IRoom : INameable
{
    /// <summary>
    /// Gets or sets the item visibility.
    /// </summary>
    bool IsVisible { get; set; }

    IEnumerable<IItem> Items { get; }

    IEnumerable<ICharacter> Characters { get; }

    void Add(IItem item);

    void Remove(IItem item);

    void Add(ICharacter character);

    void Remove(ICharacter character);
}
