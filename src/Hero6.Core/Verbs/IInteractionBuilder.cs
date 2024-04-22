// <copyright file="IInteractionBuilder.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using LateStartStudio.Hero6.Scripts;

namespace LateStartStudio.Hero6.Verbs;

public interface IInteractionBuilder<T>
    where T : IBuilder
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
