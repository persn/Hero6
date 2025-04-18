// <copyright file="IUserInterface.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.UserInterfaces;

public interface IUserInterface
{
    bool IsVisible { get; set; }

    bool PauseGame { get; } // TODO Should this be here?

    int Width { get; }

    int Height { get; }
}

public interface IUserInterfaceDesigner<T, TBuilder> : IDesigner
    where T : IUserInterface
    where TBuilder : IUserInterfaceDesigner<T, TBuilder>
{
    TBuilder OnMouseEnter(Action<T, EventArgs> interaction) => OnMouseEnter((s, e) => interaction.AsTask(s, e));

    TBuilder OnMouseEnter(Func<T, EventArgs, Task> interaction);

    TBuilder OnMouseLeave(Action<T, EventArgs> interaction) => OnMouseLeave((s, e) => interaction.AsTask(s, e));

    TBuilder OnMouseLeave(Func<T, EventArgs, Task> interaction);

    TBuilder OnMouseButtonUp(Action<T, MouseButtonInteraction> interaction) => OnMouseButtonUp((s, e) => interaction.AsTask(s, e));

    TBuilder OnMouseButtonUp(Func<T, MouseButtonInteraction, Task> interaction);
}
