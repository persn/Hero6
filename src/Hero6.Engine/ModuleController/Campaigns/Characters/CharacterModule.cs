// <copyright file="CharacterModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.InventoryItems;
using LateStartStudio.Hero6.ModuleController.Campaigns.Animations;
using LateStartStudio.Hero6.ModuleController.Campaigns.Characters.Stats;
using LateStartStudio.Hero6.ModuleController.Campaigns.Rooms;

namespace LateStartStudio.Hero6.ModuleController.Campaigns.Characters
{
    /// <summary>
    /// API for the character module.
    /// </summary>
    public abstract class CharacterModule : GameModule<ICharacterController, ICharacterModule>, ICharacterModule
    {
        /// <summary>
        /// Gets a value indicating whether this character is the player character or not.
        /// </summary>
        public bool IsPlayer => Controller.IsPlayer;

        /// <summary>
        /// Gets or sets a value indicating the movement speed of this character.
        /// </summary>
        public int Speed
        {
            get { return Controller.Speed; }
            set { Controller.Speed = value; }
        }

        /// <summary>
        /// Gets the room this character is in.
        /// </summary>
        public IRoomModule Room => Controller.Room.Module;

        /// <summary>
        /// Gets the stats for this character.
        /// </summary>
        public IStatsModule Stats => Controller.Stats.Module;

        /// <summary>
        /// Gets or sets the idle animation of this character.
        /// </summary>
        public ICharacterAnimationModule IdleAnimation
        {
            get { return Controller.IdleAnimation.Module; }
            set { Controller.IdleAnimation = ((CharacterAnimationModule)value).Controller; }
        }

        /// <summary>
        /// Gets or sets the movement animation of this character.
        /// </summary>
        public ICharacterAnimationModule MoveAnimation
        {
            get { return Controller.MoveAnimation.Module; }
            set { Controller.MoveAnimation = ((CharacterAnimationModule)value).Controller; }
        }

        /// <summary>
        /// Gets the inventory of this character.
        /// </summary>
        public IEnumerable<IInventoryItem> Inventory => Controller.Inventory;

        /// <summary>
        /// Adds inventory item to this character's inventory.
        /// </summary>
        /// <typeparam name="T">The inventory item.</typeparam>
        public void AddInventoryItem(IInventoryItem item) => Controller.AddInventoryItem(item);

        /// <summary>
        /// Removes inventory item to this character's inventory.
        /// </summary>
        /// <typeparam name="T">the inventory item.</typeparam>
        public void RemoveInventoryItem(IInventoryItem item) => Controller.RemoveInventoryItem(item);

        /// <summary>
        /// Checks if the character has inventory item.
        /// </summary>
        /// <typeparam name="T">The inventory item.</typeparam>
        /// <returns>True if the character has the inventory item. False if not.</returns>
        public bool HasInventoryItem(IInventoryItem item) => Controller.HasInventoryItem(item);

        /// <summary>
        /// Make the character walk to the input coordinates.
        /// </summary>
        /// <param name="x">The x coordinate to walk to.</param>
        /// <param name="y">The y coordinate to walk to.</param>
        public Task WalkAsync(int x, int y) => Controller.WalkAsync(x, y);

        /// <summary>
        /// Face towards the specified direciton.
        /// </summary>
        /// <param name="direction">The direction to face.</param>
        public void Face(CharacterDirection direction) => Controller.Face(direction);

        /// <summary>
        /// Face towards the coordinate.
        /// </summary>
        /// <param name="locationX">The x coordinate to face.</param>
        /// <param name="locationY">The y coordiante to face.</param>
        public void Face(int locationX, int locationY) => Controller.Face(locationX, locationY);

        /// <summary>
        /// Change the room for this character.
        /// </summary>
        /// <typeparam name="T">The room.</typeparam>
        /// <param name="x">The x coordinate the character should spawn on.</param>
        /// <param name="y">The y coordinate the character should spawn on.</param>
        /// <param name="direction">The direction the character should be facing on spwan.</param>
        public void ChangeRoom<T>(int x = 0, int y = 0, CharacterDirection direction = CharacterDirection.CenterDown) where T : IRoomModule
        {
            Controller.ChangeRoom<T>(x, y, direction);
        }

        /// <summary>
        /// Set this character as the player character of the campaign.
        /// </summary>
        public void SetAsPlayer() => Controller.SetAsPlayer();
    }
}
