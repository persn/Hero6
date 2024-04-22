// <copyright file="CharacterAnimationBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Animations;

internal class CharacterAnimationBuilder : Builder<ICharacterAnimation, CharacterAnimation>, ICharacterAnimationBuilder
{
    private readonly CharacterAnimationData data = new();

    public CharacterAnimationBuilder(string name, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        data.Name = name;
    }

    public ICharacterAnimationBuilder WithCenterDownAnimation(string animation)
    {
        data.CenterDown = animation;
        return this;
    }

    public ICharacterAnimationBuilder WithCenterUpAnimation(string animation)
    {
        data.CenterUp = animation;
        return this;
    }

    public ICharacterAnimationBuilder WithLeftCenterAnimation(string animation)
    {
        data.LeftCenter = animation;
        return this;
    }

    public ICharacterAnimationBuilder WithLeftDownAnimation(string animation)
    {
        data.LeftDown = animation;
        return this;
    }

    public ICharacterAnimationBuilder WithLeftUpAnimation(string animation)
    {
        data.LeftUp = animation;
        return this;
    }

    public ICharacterAnimationBuilder WithRightCenterAnimation(string animation)
    {
        data.RightCenter = animation;
        return this;
    }

    public ICharacterAnimationBuilder WithRightDownAnimation(string animation)
    {
        data.RightDown = animation;
        return this;
    }

    public ICharacterAnimationBuilder WithRightUpAnimation(string animation)
    {
        data.RightUp = animation;
        return this;
    }

    protected override object[] BuildParams() => [data];
}
