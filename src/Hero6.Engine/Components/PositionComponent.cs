// <copyright file="PositionComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;
using XnaPoint = Microsoft.Xna.Framework.Point;
using XnaVector2 = Microsoft.Xna.Framework.Vector2;

namespace LateStartStudio.Hero6.Components;

internal static class PositionExtensions
{
    public static XnaPoint ToMonoGame(this Point point) => new(point.X, point.Y);

    public static XnaPoint ToMonoGame(this IPositionComponent point) => new(point.X, point.Y);

    public static XnaVector2 ToVector2(this Point point) => new(point.X, point.Y);

    public static XnaVector2 ToVector2(this IPositionComponent point) => new(point.X, point.Y);

    public static Point ToDotNet(this XnaPoint point) => new(point.X, point.Y);

    public static Point ToDotNet(this IPositionComponent point) => new(point.X, point.Y);

    public static XnaVector2 ToVector2(this XnaPoint point) => new(point.X, point.Y);

    public static bool Intersects(this IPositionComponent position, Point point)
        => position.Intersects(point.X, point.Y);

    public static bool Intersects(this IPositionComponent position, XnaPoint point)
        => position.Intersects(point.X, point.Y);
}

/// <summary>
/// This component is typically used to calculate position and size for rendering
/// sprites or animations.
/// </summary>
internal interface IPositionComponent : IComponent
{
    event EventHandler PositionChanged;

    event EventHandler SizeChanged;

    int X { get; set; }

    int Y { get; set; }

    int Width { get; set; }

    int Height { get; set; }

    bool Intersects(int x, int y) =>
        x >= X &&
        x < X + Width &&
        y >= Y &&
        y < Y + Height;
}

internal class SystemDrawingPositionComponent : Component, IPositionComponent
{
    private Point position;
    private Size size;

    public event EventHandler PositionChanged;

    public event EventHandler SizeChanged;

    public int X
    {
        get => position.X;
        set
        {
            if (position.X != value)
            {
                position = new(value, position.Y);
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public int Y
    {
        get => position.Y;
        set
        {
            if (position.Y != value)
            {
                position = new(position.X, value);
                PositionChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public int Width
    {
        get => size.Width;
        set
        {
            if (size.Width != value)
            {
                size = new(value, size.Height);
                SizeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public int Height
    {
        get => size.Height;
        set
        {
            if (size.Height != value)
            {
                size = new(size.Width, value);
                SizeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}

internal class MousePositionComponent(IMouse mouse) : Component, IPositionComponent
{
    private readonly IMouse mouse = mouse;

    private Size size;

    public event EventHandler PositionChanged
    {
        add => mouse.Move += value.Invoke;
        remove => mouse.Move -= value.Invoke;
    }

    public event EventHandler SizeChanged;

    public int X
    {
        get => mouse.X;
        set => mouse.X = value;
    }

    public int Y
    {
        get => mouse.Y;
        set => mouse.Y = value;
    }

    public int Width
    {
        get => size.Width;
        set
        {
            if (size.Width != value)
            {
                size = new(value, size.Height);
                SizeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public int Height
    {
        get => size.Height;
        set
        {
            if (size.Height != value)
            {
                size = new(size.Width, value);
                SizeChanged?.Invoke(this, EventArgs.Empty);
            }
        }
    }
}
