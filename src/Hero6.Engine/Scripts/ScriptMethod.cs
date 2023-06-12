// <copyright file="ScriptMethod.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;
using System.Reflection;

namespace LateStartStudio.Hero6.Scripts;

/// <summary>
/// Container for the scripted methods that will trigger on the events specified with the
/// <see cref="ScriptEventAttribute"/>.
/// </summary>
public class ScriptMethod
{
    public ScriptMethod(Script parent, MethodInfo method)
    {
        Parent = parent;
        Method = method;
        Attribute = method.GetCustomAttribute<ScriptEventAttribute>();
    }

    /// <summary>
    /// The script that's holding this <see cref="ScriptMethod" />.
    /// </summary>
    public Script Parent { get; }

    /// <summary>
    /// The method that should be invoked when the event occurs.
    /// </summary>
    public MethodInfo Method { get; }

    /// <summary>
    /// Attribute that was assigned to the <see cref="ScriptMethod". />.
    /// </summary>
    public ScriptEventAttribute Attribute { get; }

    /// <summary>
    /// Invokes the script method.
    /// </summary>
    public void Invoke() => Invoke(Array.Empty<object>());

    /// <summary>
    /// Invokes the script method.
    /// </summary>
    public void Invoke(object[] args) => Task.Run(async () =>
    {
        if (Method.ReturnType == typeof(void))
            Method.Invoke(Parent, args);
        else
            await (Task)Method.Invoke(Parent, args);
    });
}
