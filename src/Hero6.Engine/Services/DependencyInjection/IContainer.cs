// <copyright file="IContainer.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;

namespace LateStartStudio.Hero6.Services.DependencyInjection
{
    /// <summary>
    /// Container service for Dependency Injection.
    /// </summary>
    public interface IContainer
    {
        /// <summary>
        /// Gets instance from generic type. Make sure <see cref="IContainer.Build"/> is invoked first.
        /// </summary>
        /// <typeparam name="T">The type instance to get.</typeparam>
        /// <returns>The instance that matches the type.</returns>
        T Get<T>(params (string name, object instance)[] args);

        /// <summary>
        /// Gets instance from type. Make sure <see cref="IContainer.Build"/> is invoked first.
        /// </summary>
        /// <param name="type">The type instance to get.</param>
        /// <typeparam name="T">The type instance to get.</typeparam>
        /// <returns>The instance that matches the type.</returns>
        T Get<T>(Type type, params (string name, object instance)[] args);
    }
}
