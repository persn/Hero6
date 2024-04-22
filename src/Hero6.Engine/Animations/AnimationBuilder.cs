// <copyright file="AnimationBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Animations;

internal class AnimationBuilder : Builder<IAnimation, Animation>, IAnimationBuilder
{
    private readonly AnimationData data = new();

    public AnimationBuilder(string name, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        data.Name = name;
    }

    public IAnimationBuilder WithColumns(int columns)
    {
        data.Columns = columns;
        return this;
    }

    public IAnimationBuilder WithRows(int rows)
    {
        data.Rows = rows;
        return this;
    }

    public IAnimationBuilder WithSprite(string sprite)
    {
        data.Sprite = sprite;
        return this;
    }

    protected override object[] BuildParams() => [data];
}
