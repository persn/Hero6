// <copyright file="ScriptComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Components;

namespace LateStartStudio.Hero6.Scripts;

internal interface IScriptComponent<TEvent> : IComponent where TEvent : Enum
{
    /// <summary>
    /// Invoke the event attached on this entity.
    /// </summary>
    /// <param name="event">The event to invoke.</param>
    bool Invoke(int x, int y, TEvent @event);
}

internal class ScriptComponent<TEvent>(Dictionary<TEvent, Func<Task>> events) : Component, IScriptComponent<TEvent>
    where TEvent : Enum
{
    public bool Invoke(int x, int y, TEvent @event)
    {
        if (Parent.Get<IPositionComponent>().Intersects(x, y) && events.TryGetValue(@event, out var scriptMethod))
        {
            _ = scriptMethod?.Invoke();
            return true;
        }

        return false;
    }
}
