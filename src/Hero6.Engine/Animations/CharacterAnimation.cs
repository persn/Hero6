// <copyright file="CharacterAnimation.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Characters;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Animations;

internal interface ICharacterAnimationComponent : ICharacterAnimation, IComponent
{
}

internal class CharacterAnimation : Component, ICharacterAnimationComponent, ILoadable, IDrawable
{
    private readonly CharacterAnimationData data;
    private readonly IRepository<IAnimation> repository;
    private readonly Dictionary<CharacterDirection, Animation> animations = new();

    private CharacterDirection current;

    public CharacterAnimation(CharacterAnimationData data, IRepository<IAnimation> repository)
    {
        this.data = data;
        this.repository = repository;

        AddAnimation(CharacterDirection.CenterDown, data.CenterDown);
        AddAnimation(CharacterDirection.CenterUp, data.CenterUp);
        AddAnimation(CharacterDirection.LeftCenter, data.LeftCenter);
        AddAnimation(CharacterDirection.LeftDown, data.LeftDown);
        AddAnimation(CharacterDirection.LeftUp, data.LeftUp);
        AddAnimation(CharacterDirection.RightCenter, data.RightCenter);
        AddAnimation(CharacterDirection.RightDown, data.RightDown);
        AddAnimation(CharacterDirection.RightUp, data.RightUp);
    }

    public string Name => data.Name;

    public override bool Enabled
    {
        get => base.Enabled;
        set
        {
            base.Enabled = value;
            animations[current].Enabled = value;
        }
    }

    public CharacterDirection Direction
    {
        get => current;
        set
        {
            animations[current].Enabled = false;
            current = value;
            animations[current].Enabled |= Enabled;
        }
    }

    public override void Initialize()
    {
        base.Initialize();

        foreach (var animation in animations.Values)
        {
            animation.Parent = Parent;
            animation.Initialize();
        }
    }

    public void Load()
    {
        foreach (var animation in animations.Values)
            animation.Load();
    }

    public override void Update(Microsoft.Xna.Framework.GameTime time)
    {
        base.Update(time);
        animations[current].Update(time);
    }

    public void Draw(Microsoft.Xna.Framework.GameTime gameTime) => animations[current].Draw(gameTime);

    private void AddAnimation(CharacterDirection direction, string animation)
    {
        if (!string.IsNullOrEmpty(animation))
        {
            var a = (Animation)repository[animation];
            animations.Add(direction, a);
            a.Enabled = false;
        }
    }
}
