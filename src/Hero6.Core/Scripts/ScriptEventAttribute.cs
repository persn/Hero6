// <copyright file="ScriptEventAttribute.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Scripts;

public abstract class BaseScriptEventAttribute : Attribute
{
    public BaseScriptEventAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}

/// <summary>
/// <see cref="Attribute"/> for declaring Hero6 game events.
/// </summary>
public class ScriptEventAttribute<T> : BaseScriptEventAttribute where T : Enum
{
    public ScriptEventAttribute(string name, T @event) : base(name)
    {
        Event = @event;
    }

    public T Event { get; }
}
