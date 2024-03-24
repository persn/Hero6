// <copyright file="UpdateSynchronizationContext.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Collections.Concurrent;

namespace LateStartStudio.Hero6.Threading;

/// <summary>
/// Message queue for delegates that needs to be executed on the Update event, before the Draw event.
/// </summary>
internal class UpdateSynchronizationContext : SynchronizationContext
{
    private static readonly Lazy<UpdateSynchronizationContext> LazyInstance = new(new UpdateSynchronizationContext());

    private readonly ConcurrentQueue<Action> runOnUpdate = new();

    private UpdateSynchronizationContext()
    {
    }

    internal static UpdateSynchronizationContext Instance => LazyInstance.Value;

    public override void Post(SendOrPostCallback d, object state)
    {
        runOnUpdate.Enqueue(() => d(state));
    }

    internal void Run()
    {
        while (runOnUpdate.TryDequeue(out var onUpdate))
            onUpdate();
    }
}

internal static class SynchronizationContextExtensions
{
    internal static void Post(this SynchronizationContext synchronizationContext, Action action) => synchronizationContext.Post(_ => action(), null);
}
