// <copyright file="CampaignModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    /// <summary>
    /// API for a campaign module.
    /// </summary>
    public abstract class CampaignModule : GameModule<ICampaignController, ICampaignModule>, ICampaignModule
    {
        /// <summary>
        /// Gets the stat cap for this campaign.
        /// </summary>
        public abstract int StatCap { get; }

        /// <summary>
        /// Gets or sets the player character of this campaign.
        /// </summary>
        //public ICharacter Player { get; set; }
    }
}
