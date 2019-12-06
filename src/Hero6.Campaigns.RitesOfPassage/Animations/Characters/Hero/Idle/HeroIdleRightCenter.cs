﻿// <copyright file="HeroIdleRightCenter.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.ModuleController.Campaigns.Animations;
using LateStartStudio.Hero6.Services.DependencyInjection;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Animations.Characters.Hero.Idle
{
    [Injectable]
    public class HeroIdleRightCenter : AnimationModule
    {
        public override string Name => "Hero Idle Right Center";

        public override string Sprite => "Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Right Center";

        public override int Cols => 1;

        public override int Rows => 1;
    }
}
