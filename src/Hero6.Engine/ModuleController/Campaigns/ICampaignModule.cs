// <copyright file="ICampaignModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    public interface ICampaignModule : IGameModule
    {
        int StatCap { get; }

        //ICharacter Player { get; set; }
    }
}
