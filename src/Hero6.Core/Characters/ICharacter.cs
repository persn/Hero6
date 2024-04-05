// <copyright file="ICharacter.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Animations;
using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.Repositories;
using LateStartStudio.Hero6.Rooms;

namespace LateStartStudio.Hero6.Characters;

public interface ICharacter : INameable
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
    /// Gets or sets the item visibility.
    /// </summary>
    bool IsVisible { get; set; }

    /// <summary>
    /// Gets a value indicating whether this character is the player character or not.
    /// </summary>
    bool IsPlayer { get; }

    /// <summary>
    /// Gets or sets a value indicating the movement speed of this character.
    /// </summary>
    int Speed { get; set; }

    /// <summary>
    /// Gets the room the character is currently in.
    /// </summary>
    IRoom Room { get; }

    Stats Stats { get; }

    /// <summary>
    /// Gets or sets the idle animation of this character.
    /// </summary>
    ICharacterAnimation IdleAnimation { get; set; }

    /// <summary>
    /// Gets or sets the movement animation of this character.
    /// </summary>
    ICharacterAnimation MoveAnimation { get; set; }

    /// <summary>
    /// Gets the inventory of this character.
    /// </summary>
    IEnumerable<IInventoryItem> Inventory { get; }

    /// <summary>
    /// Adds inventory item to this character's inventory.
    /// </summary>
    public void Add(IInventoryItem inventoryItem);

    /// <summary>
    /// Removes inventory item to this character's inventory.
    /// </summary>
    public void Remove(IInventoryItem inventoryItem);

    /// <summary>
    /// Checks if the character has inventory item.
    /// </summary>
    public bool Has(IInventoryItem inventoryItem);

    /// <summary>
    /// Make the character walk to the input coordinates.
    /// </summary>
    /// <param name="x">The x coordinate to walk to.</param>
    /// <param name="y">The y coordinate to walk to.</param>
    Task<bool> WalkAsync(int x, int y);

    void Face(CharacterDirection direction);

    void Face(int locationX, int locationY);

    void ChangeRoom(IRoom room, int x = 0, int y = 0, CharacterDirection direction = CharacterDirection.CenterDown);

    void SetAsPlayer();
}
