// <copyright file="LifeCycle.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.Services.DependencyInjection
{
    /// <summary>
    /// Life Cycle alternatives to services and module/controllers.
    /// </summary>
    public enum LifeCycle
    {
        /// <summary>
        /// One instance is created and this instance wil be re-used for the entire game.
        /// </summary>
        Singleton,

        /// <summary>
        /// A new instance will be created every time someone request this service/module/controller.
        /// </summary>
        Transient,
    }
}
