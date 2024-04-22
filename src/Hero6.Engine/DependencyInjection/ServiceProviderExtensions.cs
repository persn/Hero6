// <copyright file="ServiceProviderExtensions.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using Microsoft.Extensions.DependencyInjection;

namespace LateStartStudio.Hero6.DependencyInjection;

internal static class ServiceProviderExtensions
{
    public static object CreateInstance(this IServiceProvider serviceProvider, Type type, params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance(serviceProvider, type, parameters);
    }

    public static T CreateInstance<T>(this IServiceProvider serviceProvider, params object[] parameters)
    {
        return ActivatorUtilities.CreateInstance<T>(serviceProvider, parameters);
    }
}
