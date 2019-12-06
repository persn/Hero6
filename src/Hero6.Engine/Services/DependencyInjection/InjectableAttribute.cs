// <copyright file="InjectableAttribute.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

using System;

namespace LateStartStudio.Hero6.Services.DependencyInjection
{
    /// <summary>
    /// Assignable options to injectable services/modules/controllers.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
    public sealed class InjectableAttribute : Attribute
    {
        /// <summary>
        /// Gets or sets the life cycle of the service/module/controller. Defaults to <see cref="LifeCycle.Singleton"/>.
        /// </summary>
        public LifeCycle LifeCycle { get; set; } = LifeCycle.Singleton;

        /// <summary>
        /// Gets or sets whether this service/module/controller should be ignored when game loads. Defaults to false.
        /// </summary>
        public bool IgnoreOnInit { get; set; } = false;
    }
}
