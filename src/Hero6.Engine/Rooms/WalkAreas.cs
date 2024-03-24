// <copyright file="WalkAreas.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System.Drawing;

namespace LateStartStudio.Hero6.Rooms;

internal class WalkAreas
{
    private readonly string source;
    private readonly List<WalkArea> walkAreas = new();

    public WalkAreas(string source)
    {
        this.source = source;
    }

    public void Load(Func<string, IEnumerable<WalkArea>> load)
    {
        walkAreas.AddRange(load(source));
    }

    public IEnumerable<Point> GetPath(Point start, Point end)
    {
        foreach (var area in walkAreas)
        {
            if (
                !area.Nodes.ContainsKey((start.Y * area.Width) + start.X) ||
                !area.Nodes.ContainsKey((end.Y * area.Width) + end.X))
            {
                continue;
            }

            return area.GetPath(start, end);
        }

        return null;
    }
}
