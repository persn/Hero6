// <copyright file="DelegateExtensions.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Util;

internal static class DelegateExtensions
{
    internal static Task AsTask(this Action action)
    {
        action();
        return Task.CompletedTask;
    }

    internal static Task AsTask<T>(this Action<T> action, T arg)
    {
        action(arg);
        return Task.CompletedTask;
    }

    internal static Task AsTask<T1, T2>(this Action<T1, T2> action, T1 arg1, T2 arg2)
    {
        action(arg1, arg2);
        return Task.CompletedTask;
    }
}
