// <copyright file="IIntersectableComponent.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;
using XnaPoint = Microsoft.Xna.Framework.Point;
using XnaVector2 = Microsoft.Xna.Framework.Vector2;

namespace LateStartStudio.Hero6.Components;

internal static class IntersectableExtensions
{
    public static bool Intersects(this IIntersectableComponent intersectable, Point point)
        => intersectable.Intersects(point.X, point.Y);

    public static bool Intersects(this IIntersectableComponent intersectable, XnaPoint point)
        => intersectable.Intersects(point.X, point.Y);
}

internal interface IIntersectableComponent : IComponent
{
    bool Intersects(int x, int y);
}
