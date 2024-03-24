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
    /// <summary>
    /// Invoke the event attached on this entity.
    /// </summary>
    /// <param name="event">The event to invoke.</param>
    bool Invoke(int x, int y, TEvent @event);
}

internal class ScriptComponent<TEvent> : Component, IScriptComponent<TEvent>
    where TEvent : Enum
{
    private readonly IScriptRepository repository;
    private readonly Dictionary<TEvent, ScriptMethod> events = new();

    public ScriptComponent(IScriptRepository repository)
    {
        this.repository = repository;
    }

    public bool Invoke(int x, int y, TEvent @event)
    {
        if (Parent.Get<IPositionComponent>().Intersects(x, y) && events.TryGetValue(@event, out var scriptMethod))
        {
            scriptMethod?.Invoke();
            return true;
        }

        return false;
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
                    var @event = itemAttribute.Event;
                    events[@event] = scriptMethod;
                }
            }
        }
        else
            throw new Exception(@$"Parent ""{Parent.GetType().Name}"" must implement interface ""{nameof(INameable)}"" in order to be scriptable.");
    }
}
