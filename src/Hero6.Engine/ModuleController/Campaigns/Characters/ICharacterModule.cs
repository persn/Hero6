// <copyright file="ICharacterModule.cs" company="Late Start Studio">
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
    public interface ICharacterModule : IGameModule
    {
        bool IsPlayer { get; }

        int Speed { get; set; }

        IRoomModule Room { get; }

        IStatsModule Stats { get; }

        ICharacterAnimationModule IdleAnimation { get; set; }

        ICharacterAnimationModule MoveAnimation { get; set; }

        IEnumerable<IInventoryItem> Inventory { get; }

        void AddInventoryItem(IInventoryItem item);

        void RemoveInventoryItem(IInventoryItem item);

        bool HasInventoryItem(IInventoryItem item);

        Task WalkAsync(int x, int y);

        void Face(CharacterDirection direction);

        void Face(int locationX, int locationY);

        void ChangeRoom<T>(int x = 0, int y = 0, CharacterDirection direction = CharacterDirection.CenterDown) where T : IRoomModule;

        void SetAsPlayer();
    }
}
