// <copyright file="Hero.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Animations;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Animations.Characters;

internal class Hero : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<ICharacterAnimationBuilder>("Hero Idle", builder => builder
            .WithCenterDownAnimation("Hero Idle Center Down")
            .WithCenterUpAnimation("Hero Idle Center Up")
            .WithLeftCenterAnimation("Hero Idle Left Center")
            .WithLeftDownAnimation("Hero Idle Left Down")
            .WithLeftUpAnimation("Hero Idle Left Up")
            .WithRightCenterAnimation("Hero Idle Right Center")
            .WithRightDownAnimation("Hero Idle Right Down")
            .WithRightUpAnimation("Hero Idle Right Up"))

        .Add<ICharacterAnimationBuilder>("Hero Walk", builder => builder
            .WithCenterDownAnimation("Hero Walk Center Down")
            .WithCenterUpAnimation("Hero Walk Center Up")
            .WithLeftCenterAnimation("Hero Walk Left Center")
            .WithLeftDownAnimation("Hero Walk Left Down")
            .WithLeftUpAnimation("Hero Walk Left Up")
            .WithRightCenterAnimation("Hero Walk Right Center")
            .WithRightDownAnimation("Hero Walk Right Down")
            .WithRightUpAnimation("Hero Walk Right Up"))

        .Add<IAnimationBuilder>("Hero Idle Center Down", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Center Down")
            .WithColumns(1).WithRows(1))
        .Add<IAnimationBuilder>("Hero Idle Center Up", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Center Up")
            .WithColumns(1).WithRows(1))
        .Add<IAnimationBuilder>("Hero Idle Left Center", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Left Center")
            .WithColumns(1).WithRows(1))
        .Add<IAnimationBuilder>("Hero Idle Left Down", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Left Down")
            .WithColumns(1).WithRows(1))
        .Add<IAnimationBuilder>("Hero Idle Left Up", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Left Up")
            .WithColumns(1).WithRows(1))
        .Add<IAnimationBuilder>("Hero Idle Right Center", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Right Center")
            .WithColumns(1).WithRows(1))
        .Add<IAnimationBuilder>("Hero Idle Right Down", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Right Down")
            .WithColumns(1).WithRows(1))
        .Add<IAnimationBuilder>("Hero Idle Right Up", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Idle/Right Up")
            .WithColumns(1).WithRows(1))

        .Add<IAnimationBuilder>("Hero Walk Center Down", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Center Down")
            .WithColumns(9).WithRows(1))
        .Add<IAnimationBuilder>("Hero Walk Center Up", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Center Up")
            .WithColumns(8).WithRows(1))
        .Add<IAnimationBuilder>("Hero Walk Left Center", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Left Center")
            .WithColumns(9).WithRows(1))
        .Add<IAnimationBuilder>("Hero Walk Left Down", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Left Down")
            .WithColumns(11).WithRows(1))
        .Add<IAnimationBuilder>("Hero Walk Left Up", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Left Up")
            .WithColumns(7).WithRows(1))
        .Add<IAnimationBuilder>("Hero Walk Right Center", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Right Center")
            .WithColumns(8).WithRows(1))
        .Add<IAnimationBuilder>("Hero Walk Right Down", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Right Down")
            .WithColumns(11).WithRows(1))
        .Add<IAnimationBuilder>("Hero Walk Right Up", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Hero/Walk/Right Up")
            .WithColumns(7).WithRows(1))
        ;
}
