// <copyright file="GameController.cs" company="Late Start Studio">
// Copyright (C) Late Start Studio
// This file is subject to the terms and conditions of the MIT license specified in the file
// 'LICENSE.CODE.md', which is a part of this source code package.
// </copyright>

namespace LateStartStudio.Hero6.ModuleController.Campaigns
{
    /// <summary>
    /// API for all game enttities.
    /// </summary>
    /// <typeparam name="TController">The Controlle type.</typeparam>
    /// <typeparam name="TModule">The Module type.</typeparam>
    public abstract class GameController<TController, TModule> : Controller<TController, TModule>, IGameController
        where TController : IGameController
        where TModule : IGameModule
    {
        /// <summary>
        /// Makes a new instance of the <see cref="GameController{TController,TModule}"/> class.
        /// </summary>
        /// <param name="module">The module corresponding to this controller.</param>
        protected GameController(TModule module, IServiceProvider services)
            : base(module, services)
        {
        }
    }
}
