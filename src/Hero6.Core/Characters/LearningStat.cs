// <copyright file="LearningStat.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Characters;

public class LearningStat
{
    private int buffer;

    internal LearningStat()
    {
    }

    public event EventHandler<EventArgs> Change;

    public int Current => TriangularReverse(Buffer);

    private int Buffer
    {
        get
        {
            return buffer;
        }

        set
        {
            var oldStat = Current;
            buffer = value;
            var newStat = Current;

            if (oldStat != newStat)
            {
                Change?.Invoke(this, EventArgs.Empty);
            }
        }
    }

    public void Train(int factor)
    {
        var newBuffer = Buffer + factor;
        Buffer = TriangularReverse(newBuffer) >= Stats.Cap ? Triangular(Stats.Cap) : newBuffer;
    }

    /// <summary>
    /// https://en.wikipedia.org/wiki/Triangular_number.
    /// </summary>
    /// <returns></returns>
    private static int Triangular(int n) => (n * (n + 1)) / 2;

    /// <summary>
    /// https://en.wikipedia.org/wiki/Triangular_number.
    /// </summary>
    /// <returns></returns>
    private int TriangularReverse(int n) => (int)(Math.Sqrt((8.0 + n) + 1.0) / 2.0);
}
