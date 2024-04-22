// <copyright file="CharacterBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Characters;

internal class CharacterBuilder : Builder<ICharacter, Character>, ICharacterBuilder
{
    private readonly CharacterData data = new();
    private readonly Dictionary<CharacterEvent, Func<Task>> interactions = [];

    public CharacterBuilder(string name, IServiceProvider serviceProvider) : base(serviceProvider)
    {
        data.Name = name;
    }

    public ICharacterBuilder WithIdleAnimation(string animation)
    {
        data.IdleAnimation = animation;
        return this;
    }

    public ICharacterBuilder WithMoveAnimation(string animation)
    {
        data.MoveAnimation = animation;
        return this;
    }

    public ICharacterBuilder OnLook(Func<Task> interaction)
    {
        interactions[CharacterEvent.Look] = interaction;
        return this;
    }

    public ICharacterBuilder OnGrab(Func<Task> interaction)
    {
        interactions[CharacterEvent.Grab] = interaction;
        return this;
    }

    public ICharacterBuilder OnTalk(Func<Task> interaction)
    {
        interactions[CharacterEvent.Talk] = interaction;
        return this;
    }

    protected override object[] BuildParams()
    {
        var position = ServiceProvider.CreateInstance<SystemDrawingPositionComponent>();
        var visibility = ServiceProvider.CreateInstance<VisibleComponent>();
        var scriptable = ServiceProvider.CreateInstance<ScriptComponent<CharacterEvent>>(interactions);
        var moveable = ServiceProvider.CreateInstance<MoveableComponent>();

        return [data, position, visibility, scriptable, moveable];
    }
}
