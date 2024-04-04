// <copyright file="EventHandlerAsync.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Threading;

/// <summary>
/// Async delegate to be used with event handlers.
/// </summary>
/// <typeparam name="TEventArgs"></typeparam>
/// <param name="sender"></param>
/// <param name="eventArgs"></param>
/// <returns></returns>
internal delegate Task EventHandlerAsync<TEventArgs>(object sender, TEventArgs eventArgs) where TEventArgs : EventArgs;

internal static class EventHandlerAsyncExtensions
{
    public static async Task InvokeAsync<TEventArgs>(this EventHandlerAsync<TEventArgs> eventHandler, object sender, TEventArgs eventArgs) where TEventArgs : EventArgs
    {
        await Task.WhenAll(eventHandler
            .GetInvocationList()
            .Cast<EventHandlerAsync<EventArgs>>()
            .Select(async d => await d.Invoke(sender, eventArgs)));
    }
}
