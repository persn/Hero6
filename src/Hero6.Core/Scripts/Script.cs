// <copyright file="Script.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Scripts;

/// <summary>
/// Scripts are the way to communicate with the game logic. Make a class that inherits from <see cref="Script"/> and
/// write methods that are annotated with implementations of the <see cref="ScriptEventAttribute"/>.
/// </summary>
/// <remarks>
/// As a design choice we want to keep the abstract version of this class as empty as possible. This keeps the script
/// interface as clean as possible which reduces boilerplate code for the game developers, and it also allows the
/// engine developers flexibility because they'll have less code to maintain compatability for.
/// </remarks>
public abstract class Script
{
    public virtual void Initialize()
    {
    }

    public virtual Task InitializeAsync() => Task.CompletedTask;
}
