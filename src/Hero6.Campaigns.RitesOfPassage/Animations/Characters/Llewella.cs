// <copyright file="Llewella.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Animations;

namespace LateStartStudio.Hero6.Campaigns.RitesOfPassage.Animations.Characters;

internal class Llewella : Script
{
    public override void Design(IDesigner designer) => designer
        .Add<ICharacterAnimationBuilder>("Llewella Idle", builder => builder
            .WithCenterDownAnimation("Llewella Idle Center Down"))

        .Add<IAnimationBuilder>("Llewella Idle Center Down", builder => builder
            .WithSprite("Campaigns/Rites of Albion/Animations/Characters/Llewella/Idle/Center Down")
            .WithColumns(1).WithRows(1))
        ;
}
