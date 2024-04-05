// <copyright file="Stat.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Characters;

public class Stat
{
    private int current;

    internal Stat()
    {
    }

    public event EventHandler<EventArgs> Change;

    public int Current
    {
        get => current;

        set
        {
            current = value > Stats.Cap ? Stats.Cap : value;
            Change?.Invoke(this, EventArgs.Empty);
        }
    }
}
