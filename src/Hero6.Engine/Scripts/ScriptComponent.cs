// <copyright file="ScriptComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Repositories;

namespace LateStartStudio.Hero6.Scripts;

internal interface IScriptComponent<TEvent> : IComponent where TEvent : Enum
{
    void Invoke(TEvent @event);
}

internal class ScriptComponent<TParent, TEvent> : Component, IScriptComponent<TEvent>
    where TEvent : Enum
{
    private readonly IScriptRepository repository;
    private readonly Dictionary<TEvent, Action> events = new();

    public ScriptComponent(IScriptRepository repository)
    {
        this.repository = repository;
    }

    public void Invoke(TEvent @event)
    {
        if (events.TryGetValue(@event, out var scriptMethod))
            scriptMethod?.Invoke();
    }

    public override void Initialize()
    {
        base.Initialize();

        if (Parent is INameable nameable)
        {
            foreach (var scriptMethod in repository.GetScriptMethods(nameable.Name))
            {
                if (scriptMethod.Attribute is ScriptEventAttribute<TEvent> itemAttribute)
                {
                    events[itemAttribute.Event] = () => scriptMethod.Invoke();
                }
            }
        }
        else
            throw new Exception(@$"Parent ""{Parent.GetType().Name}"" must implement interface ""{nameof(INameable)}"" in order to be scriptable.");
    }
}
