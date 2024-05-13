// <copyright file="MoveableComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Diagnostics;
using LateStartStudio.Hero6.Components;
using LateStartStudio.Hero6.Rooms;
using LateStartStudio.Hero6.Threading;
using Microsoft.Extensions.Logging;
using Microsoft.Xna.Framework;

namespace LateStartStudio.Hero6.Characters;

internal interface IMoveableComponent : IComponent
{
    event EventHandlerAsync<EventArgs> IsMovingChanged;

    /// <summary>
    /// Gets whether the component is moving or not.
    /// </summary>
    bool IsMoving { get; }

    /// <summary>
    /// Gets or sets the speed.
    /// </summary>
    // TODO Find out what the integer represents as a movable speed and document it so people can understand it.
    int Speed { get; set; }

    /// <summary>
    /// Move to the destination at the input coordinates.
    /// </summary>
    /// <returns>
    /// A task that completes when the entity have reached the destination.
    /// </returns>
    Task<bool> MoveAsync(int x, int y);

    /// <summary>
    /// Stops in progress movement.
    /// </summary>
    void Stop();
}

internal class MoveableComponent(ILogger<MoveableComponent> logger) : Component, IMoveableComponent
{
    private readonly ILogger logger = logger;
    private readonly Queue<System.Drawing.Point> path = new();

    private Character parent;
    private TaskCompletionSource<bool> currentWalkTask;
    private double pixelsToMove;

    public event EventHandlerAsync<EventArgs> IsMovingChanged;

    public bool IsMoving => path.Any();

    public int Speed { get; set; } = 60;

    public async Task<bool> MoveAsync(int x, int y)
    {
        // Clearing current path must happen before we calculate a new walk path asynchronously, because
        // if the character moves even a single pixel from the current position while the calculation is
        // processing, the logic can encounter unexpected situations, and in worst case crash.
        ForceStop();

        // Moving is handled asynchronously in 2 parts
        // Part 1: Calculate path
        var timePathCalculated = Stopwatch.StartNew();
        TaskCompletionSource<bool> walkTask = currentWalkTask = new();
        var from = new System.Drawing.Point(parent.FeetX, parent.FeetY);
        var to = new System.Drawing.Point(x, y);

        // Calculating paths can be computation intensive, especially if we want to do it for multiple
        // characters at the same time, so offloading the work to the thread pool will help prevent stuttering
        var pathFound = await Task.Run(() =>
        {
            try
            {
                var newPath = ((Room)parent.Room).WalkAreas.GetPath(from, to);

                if (newPath != null && currentWalkTask == walkTask)
                {
                    foreach (var p in newPath.Skip(1)) // We're already standing at the first pixel so skip it
                        path.Enqueue(p);

                    return true;
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, $"{e.GetType()} when calculating walkable path for character {parent.Name} from {from} to {to}.");
                walkTask.SetException(e);
            }

            return false;
        });

        if (!pathFound)
            walkTask.SetResult(pathFound);

        timePathCalculated.Stop();
        logger.LogDebug(@"Path for ""{parent}"" in ""{room}"" was calculated in {time}ms", parent.Name, parent.Room.Name, timePathCalculated.ElapsedMilliseconds);

        // Part 2: Move
        await (IsMovingChanged?.InvokeAsync(this, EventArgs.Empty) ?? Task.CompletedTask);
        var res = await walkTask.Task;
        await (IsMovingChanged?.InvokeAsync(this, EventArgs.Empty) ?? Task.CompletedTask);

        return res;
    }

    public void Stop()
    {
        if (IsMoving)
            ForceStop();
    }

    public override void Initialize()
    {
        base.Initialize();
        parent = (Character)Parent;
    }

    public override void Update(GameTime time)
    {
        base.Update(time);

        if (!path.Any())
        {
            return;
        }

        pixelsToMove += Speed * time.ElapsedGameTime.TotalSeconds;
        var pixelsToMoveFloor = (int)pixelsToMove;
        pixelsToMove -= pixelsToMoveFloor;

        for (var i = 0; i < pixelsToMoveFloor; i++)
        {
            var newLocation = path.Dequeue();
            parent.Face(newLocation);
            parent.FeetX = newLocation.X;
            parent.FeetY = newLocation.Y;

            if (!path.Any())
            {
                pixelsToMove = 0.0;
                ForceStop();
                break;
            }
        }
    }

    private void ForceStop()
    {
        path.Clear();
        currentWalkTask?.TrySetResult(true);
        currentWalkTask = null;
    }
}
