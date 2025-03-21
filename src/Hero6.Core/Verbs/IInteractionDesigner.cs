// <copyright file="IInteractionDesigner.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Verbs;

public interface IInteractionDesigner<T>
{
    T OnLook(Action interaction) => OnLook(interaction.AsTask);

    T OnLook(Func<Task> interaction);

    T OnGrab(Action interaction) => OnGrab(interaction.AsTask);

    T OnGrab(Func<Task> interaction);

    T OnTalk(Action interaction) => OnTalk(interaction.AsTask);

    T OnTalk(Func<Task> interaction);
}

internal static class DelegateExtensions
{
    internal static Task AsTask(this Action action)
    {
        action();
        return Task.CompletedTask;
    }
}
