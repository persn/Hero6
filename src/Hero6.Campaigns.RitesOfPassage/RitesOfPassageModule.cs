// <copyright file="RitesOfPassageModule.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.ModuleController.Campaigns;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage
{
    public class RitesOfPassageModule : CampaignModule
    {
        public override string Name => "Rites of Passage";

        public override int StatCap => 100;

        public override void Initialize()
        {
            base.Initialize();
            API.Characters.Hero.SetAsPlayer();
        }
    }
}
