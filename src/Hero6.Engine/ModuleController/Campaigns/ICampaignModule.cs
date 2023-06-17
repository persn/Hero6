// <copyright file="ICampaignModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.ModuleController.Campaigns.Animations;
using LateStartStudio.Hero6.ModuleController.Campaigns.Characters;
using LateStartStudio.Hero6.ModuleController.Campaigns.Rooms;

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    public interface ICampaignModule : IGameModule
    {
        int StatCap { get; }

        ICharacterModule Player { get; set; }

        IAnimationModule GetAnimation<T>() where T : AnimationModule;

        ICharacterAnimationModule GetCharacterAnimation<T>() where T : CharacterAnimationModule;

        ICharacterModule GetCharacter<T>() where T : CharacterModule;

        IRoomModule GetRoom<T>() where T : RoomModule;
    }
}
