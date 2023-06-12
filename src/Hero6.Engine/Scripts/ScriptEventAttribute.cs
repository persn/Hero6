// <copyright file="ScriptEventAttribute.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Scripts;

/// <summary>
/// <see cref="Attribute"/> for declaring Hero6 scriptable game events.
/// </summary>
public abstract class ScriptEventAttribute : Attribute
{
    protected ScriptEventAttribute(string name)
    {
        Name = name;
    }

    public string Name { get; }
}
